<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BankDepositClearingCreate.aspx.cs" Inherits="DealerManagementSystem.ViewFinance.BankDepositClearingCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#DivTechnician").click(function () {
                $("#pnlTechnicianInformation").toggle(function () {
                    $(this).animate({ height: '150px', });
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--  <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="position: fixed; top: 35%; right: 46%" Width="100px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
    <div class="container IC_ticketManageInfo">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

                    <asp:TabContainer ID="tbpVendorReg" runat="server">
                        <asp:TabPanel ID="tbpnlHeader" runat="server" HeaderText="Single">
                            <ContentTemplate>
                                <asp:Panel ID="pnlSingle" runat="server">
                                    <table id="txnHistory5:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
                                        <tr>
                                            <td>
                                                <div class="boxHead">
                                                    <div class="logheading">Bank Deposit Clearing</div>
                                                    <div style="float: right; padding-top: 0px">
                                                    </div>
                                                </div>
                                                <div class="rf-p " id="txnHistory5:inputFiltersPanel">
                                                    <div class="rf-p-b " id="txnHistory5:inputFiltersPanel_body">
                                                        <asp:Panel ID="pnlCreate" runat="server">
                                                            <table class="labeltxt fullWidth">
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label71" runat="server" Text="Code" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" AutoPostBack="true" />
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label20" runat="server" Text="Bank Account" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">

                                                                                <asp:DropDownList ID="ddlBankAccount" runat="server" CssClass="TextBox" AutoPostBack="true">
                                                                                    <asp:ListItem Value="GL-167130-SBI OCC-10308201660">GL-167130-SBI OCC-10308201660</asp:ListItem>
                                                                                    <asp:ListItem Value="GL-167200-HDFC-50200017701201">GL-167200-HDFC-50200017701201</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label10" runat="server" Text="Transaction Date" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTransactionDate" PopupButtonID="txtTransactionDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtTransactionDate" WatermarkText="DD/MM/YYYY" Enabled="True"></asp:TextBoxWatermarkExtender>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label1" runat="server" Text="Value Date" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtValueDate" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtValueDate" PopupButtonID="txtValueDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtValueDate" WatermarkText="DD/MM/YYYY" Enabled="True"></asp:TextBoxWatermarkExtender>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label12" runat="server" Text="Bank Description" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtBankDescription" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label13" runat="server" Text="Branch Code" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label46" runat="server" Text="Amount" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel1" runat="server" Height="20px"></asp:Panel>
                                                        <asp:Panel ID="pnlEdit" runat="server">
                                                            <table class="labeltxt fullWidth">
                                                                <tr>
                                                                     <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Deposit For"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:DropDownList ID="ddlDepositFor" runat="server" CssClass="TextBox">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                    <asp:ListItem Value="M-Machine Advance">M-Machine Advance</asp:ListItem>
                                                                                    <asp:ListItem Value="P-Spare Parts Advance">P-Spare Parts Advance</asp:ListItem>
                                                                                    <asp:ListItem Value="S-Service Advance">S-Service Advance</asp:ListItem>
                                                                                    <asp:ListItem Value="O-Other Advance">O-Other Advance</asp:ListItem>
                                                                                    <asp:ListItem Value="Against  Invoice">Against Invoice</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label14" runat="server" Text="Is Multiple Customer" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:CheckBox ID="cbIsMultipleCustomer" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </td>

                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label45" runat="server" Text="Customer" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                   
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label15" runat="server" Text="Invoice Number" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label16" runat="server" Text="PO/SO Number" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtPONumber" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                   <%-- <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label17" runat="server" Text="SO Number" CssClass="label"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtSONumber" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label23" runat="server" CssClass="label" Text="Machine Model"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtMachineModel" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label24" runat="server" CssClass="label" Text="Department"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="TextBox">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                    <asp:ListItem Value="Marketing">Marketing</asp:ListItem>
                                                                                    <asp:ListItem Value="Spare Parts">Spare Parts</asp:ListItem>
                                                                                    <asp:ListItem Value="Service">Service</asp:ListItem>
                                                                                    <asp:ListItem Value="Sales">Sales</asp:ListItem>
                                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label18" runat="server" CssClass="label" Text="Bill Detail Given By"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtBillDetailGivenBy" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label25" runat="server" CssClass="label" Text="Bill Detail Given On"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtBillDetailGivenOn" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBillDetailGivenOn" PopupButtonID="txtBillDetailGivenOn" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBillDetailGivenOn" WatermarkText="DD/MM/YYYY" Enabled="True"></asp:TextBoxWatermarkExtender>

                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label21" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Place"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtPlace" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label51" runat="server" CssClass="label" Text="State"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="TextBox" />
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label52" runat="server" CssClass="label" Text="Region"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="TextBox" />
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <%--<td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Reference No"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>--%>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label19" runat="server" CssClass="label" Text="Header Text"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtHeaderText" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label22" runat="server" CssClass="label" Text="Assignment"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtAssignment" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <%--    <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label11" runat="server" CssClass="label" Text="Remitter Account"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterAccount" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Remitter Name"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterName" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Remitter Email"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterEmail" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Remitter Mobile"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterMobile" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Remitter Bank"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterBank" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="tbl-row-left">
                                                                            <div class="tbl-col-left">
                                                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Remitter IFSC"></asp:Label>
                                                                            </div>
                                                                            <div class="tbl-col-right">
                                                                                <asp:TextBox ID="txtRemitterIFSC" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>--%>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Button ID="btnSave" runat="server" CssClass="InputButton" OnClick="btnSave_Click" Text="Save" UseSubmitBehavior="true" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>

                        <asp:TabPanel ID="tbpnlAddress" runat="server" HeaderText="Multiple">
                            <ContentTemplate>
                                <asp:Panel ID="pnlMultiple" runat="server">
                                    <asp:FileUpload ID="fu" runat="server" />
                                    <asp:Button ID="btnExelRead" runat="server" Text="Read Data from Excel" CssClass="InputButton" OnClick="btnExelRead_Click" />
                                    <asp:LinkButton ID="lbBankDepositClearingTemplate" runat="server" OnClick="lbBankDepositClearingTemplate_Click" Style="position: relative; top: -9px; left: 0px;">Download Bank Deposit Clearing Template</asp:LinkButton>
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
                                                        <asp:GridView ID="gvBank" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="BankDepositClearingID" PageSize="20">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Bank Deposit Clearing ID">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "BankDepositClearingID")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dealer">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dealer Name">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ajax Bank Account">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAjaxBankAccount" Text='<%# DataBinder.Eval(Container.DataItem, "AjaxBankAccount")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transaction Date">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransactionDate" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionDate","{0:d}")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank Detail">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBankDetail" Text='<%# DataBinder.Eval(Container.DataItem, "BankDetail")%>' runat="server"></asp:Label>
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
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </span>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:Button ID="btnSaveMultiple" runat="server" CssClass="InputButton" OnClick="btnSaveMultiple_Click" Text="Save" UseSubmitBehavior="true" />
                                </asp:Panel>
                            </ContentTemplate>

                        </asp:TabPanel>
                    </asp:TabContainer>
                </div>
            </div>
        </div>
    </div>
    <%--    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbBankDepositClearingTemplate" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>