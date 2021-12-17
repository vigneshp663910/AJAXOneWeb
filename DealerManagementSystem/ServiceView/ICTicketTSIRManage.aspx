<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketTSIRManage.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketTSIRManage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<%--<%@ Register Src="~/UserControls/DMS_ICTicketMaterialCharges.ascx" TagPrefix="UC" TagName="DMS_ICTicketMaterialCharges" %>--%>

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
    <asp:Panel ID="pnlTSIRManage" runat="server">
        <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <div class="boxHead">
                        <div class="logheading">Filter : IC Ticket TSIR Manage</div>
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
                                    <asp:DropDownList ID="ddlDealerCode" runat="server" Width="250px" />
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label5" runat="server" Text="Customer Code"></asp:Label>
                                    <asp:TextBox ID="txtCustomerCode" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label2" runat="server" Text="TSIR No"></asp:Label>
                                    <asp:TextBox ID="txtTSIRNo" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label7" runat="server" Text="TSIR Date From "></asp:Label>
                                    <asp:TextBox ID="txtTSIRDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTSIRDateFrom" PopupButtonID="txtTSIRDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtTSIRDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label8" runat="server" Text="TSIR Date To"></asp:Label>
                                    <asp:TextBox ID="txtTSIRDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTSIRDateTo" PopupButtonID="txtTSIRDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTSIRDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="lblPlant" runat="server" Text="IC Ticket "></asp:Label>
                                    <asp:TextBox ID="txtICTicketNumber" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label3" runat="server" Text="IC Ticket Date From"></asp:Label>
                                    <asp:TextBox ID="txtICLoginDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label4" runat="server" Text="IC Ticket Date To"></asp:Label>
                                    <asp:TextBox ID="txtICLoginDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label6" runat="server" Text="SRO Code"></asp:Label>
                                    <asp:TextBox ID="txtSroCode" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label9" runat="server" Text="Type Of Warranty"></asp:Label>
                                    <asp:DropDownList ID="ddlTypeOfWarranty" runat="server" />
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="display: none">
                                    <asp:Label ID="Label10" runat="server" Text="Model"></asp:Label>
                                    <asp:DropDownList ID="ddlModelID" runat="server" Width="250px" />
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label11" runat="server" Text="Machine Serial Number"></asp:Label>
                                    <asp:TextBox ID="txtMachineSerialNumber" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                    <asp:Label ID="Label20" runat="server" Text="TSIR Status"></asp:Label>
                                    <asp:DropDownList ID="ddlTsirStatus" runat="server" />
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
                                                        <td>IC Ticket TSIR Manage</td>
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
                                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="TsirID" PageSize="20" OnRowDataBound="gvICTickets_RowDataBound" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="IC Ticket">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketNumber")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IC Ticket Date">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TSIR">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TsirNumber")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tsir Date">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "TsirDate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Machine Serial Number">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentSerialNo")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HMR">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.CurrentHMRValue")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nature Of Complaint">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "NatureOfFailures")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Priority" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServicePriority.ServicePriority")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type Of Warranty">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.TypeOfWarranty.TypeOfWarranty")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Margin Warranty" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ICTicket.IsMarginWarranty")%>' Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HO Comments1" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Label ID="lblQualityComments" Text='<%# DataBinder.Eval(Container.DataItem, "QualityComments")%>' runat="server"></asp:Label>
                                                                    <asp:TextBox ID="txtQualityComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "QualityComments")%>' CssClass="input" Width="70px" Visible="false" />
                                                                </td>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Button ID="btnQualityCommentsSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnQualityCommentsSave_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HO Comments2" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Label ID="lblServiceComments" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceComments")%>' runat="server"></asp:Label>
                                                                    <asp:TextBox ID="txtServiceComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceComments")%>' CssClass="input" Width="70px" Visible="false" />
                                                                </td>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Button ID="btnServiceCommentsSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnServiceCommentsSave_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approve / Reject" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:TextBox ID="txtStatusRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StatusRemarks")%>' CssClass="input" Width="70px" Visible="false" />
                                                                </td>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnApprove_Click" />
                                                                </td>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnReject_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send Mail" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:TextBox ID="txtCustomerEmailID" runat="server" CssClass="input" Width="200px" />
                                                                </td>
                                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                    <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSendMail_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TSIR-PDF">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibPDF" runat="server" Width="20px" ImageUrl="~/FileFormat/Pdf_Icon.jpg" OnClick="ibPDF_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnView_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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
    </asp:Panel>
    <asp:Panel ID="pnlTSIRView" runat="server" Visible="false">
        <UC:UC_BasicInformation ID="UC_BasicInformation" runat="server"></UC:UC_BasicInformation>

        <table id="txnHistory1:panelGridid5" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <div class="boxHead">
                        <div class="logheading">Material Charges</div>
                        <div style="float: right; padding-top: 0px">
                            <a href="javascript:collapseExpandMaterialCharges();">
                                <img id="imgMaterialCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                        </div>
                    </div>

                    <asp:Panel ID="pnlMaterialCharges" runat="server">
                        <div class="rf-p " id="txnHistory:inputFiltersPanel5">
                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body5">
                                <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-Width="30px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Desc">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material S/N">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prime Faulty Part">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FLD Material">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FLD Material S/N">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefectiveMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recomened Parts">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbRecomenedParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsRecomenedParts")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Base Price">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate> 
                                                 <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quotation  Parts">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbQuotationParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuotationParts")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialSource" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSource")%>' runat="server"></asp:Label>
                                                 <asp:Label ID="Label24" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSourceID")%>' runat="server"  Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlMaterialSourceF" runat="server" CssClass="TextBox" Width="80px" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtn No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery No.">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim No.">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>


        <table id="txnHistory2:panelGridid" style="height: 100%; width: 100%">
            <tr>
                <td>

                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>IC Ticket TSIR View</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>



                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label18" runat="server" Text="TSIR Number"></asp:Label>
                                <asp:TextBox ID="txtTsirNumber" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label19" runat="server" Text="TSIR Status"></asp:Label>
                                <asp:TextBox ID="txtTsirStatus" runat="server" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label13" runat="server" Text="SRO Code"></asp:Label>
                                <asp:TextBox ID="txtServiceCharge" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label14" runat="server" Text="Nature Of Failures"></asp:Label>
                                <asp:TextBox ID="txtNatureOfFailures" runat="server" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label15" runat="server" CssClass="label" Text="How Was Problem Noticed / Who  / When"></asp:Label>
                                <asp:TextBox ID="txtProblemNoticedBy" runat="server" TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label28" runat="server" CssClass="label" Text="Under What Condition Did The Failure Taken Place"></asp:Label>
                                <asp:TextBox ID="txtUnderWhatConditionFailureTaken" runat="server" TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label16" runat="server" CssClass="label" Text="Failure Details"></asp:Label>
                                <asp:TextBox ID="txtFailureDetails" runat="server" TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label22" runat="server" CssClass="label" Text="Points Checked"></asp:Label>
                                <asp:TextBox ID="txtPointsChecked" runat="server" TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Possible Root Causes"></asp:Label>
                                <asp:TextBox ID="txtPossibleRootCauses" runat="server" TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label23" runat="server" CssClass="label" Text="Specific Points Noticed"></asp:Label>
                                <asp:TextBox ID="txtSpecificPointsNoticed" runat="server" TextMode="MultiLine" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label21" runat="server" CssClass="label" Text="Parts Invoice Number"></asp:Label>
                                <asp:TextBox ID="txtPartsInvoiceNumber" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div class="col2">
            <div class="rf-p " id="txnHistory3:j_idt1289">
                <div class="rf-p-b " id="txnHistory3:j_idt1289_body">
                    <table id="txnHistory3:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <span id="txnHistory4:refreshDataGroup">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>TSIR Message</td>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="background-color: white" class="tablefixedWidth">
                                        <asp:GridView ID="gvTSIRMessage" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" DataKeyNames="TSIRMessageID" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Message">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTSIRMessage" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTSIRMessage" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="400"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display To Dealer">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisplayToDealer" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayToDealer")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="cbDisplayToDealer" runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Created By">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTSIRMessageAdd" runat="server" OnClick="lblTSIRMessageAdd_Click">Add</asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn","{0:d}")%>' runat="server"></asp:Label>
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
        <table id="txnHistory11:panelGridid" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <%-- <div class="boxHead">
                        <div class="logheading">Filter : IC Ticket TSIR Manage</div>
                        <div style="float: right; padding-top: 0px"> 
                        </div>
                    </div>--%>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                    <asp:Button ID="btnChecked" runat="server" Text="Checked" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnChecked_Click" />
                                    &nbsp; &nbsp;
                                   
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnBack_Click" /> 
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnReject_Click1" BackColor="Red" />
                                </div>
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                     <asp:Button ID="btnSendBack" runat="server" Text="Send Back" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSendBack_Click"  BackColor="Blue"  />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPdfView" runat="server">
        <%-- <rsweb:ReportViewer ID="rvPdf" runat="server">
         </rsweb:ReportViewer>--%>
    </asp:Panel>

    <script type="text/javascript">
        $(document).ready(function () {
            var gvTickets = document.getElementById('MainContent_gvTSIRMessage');

            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                    var lblNoteType = document.getElementById('MainContent_gvTSIRMessage_lblTSIRMessage_' + i);
                    if (lblNoteType != null) {
                        if (lblNoteType.innerHTML == "") {
                            lblNoteType.parentNode.parentNode.style.display = "none";
                        }
                    }
                }
            }
        });
    </script>
</asp:Content>