<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketTSIRMessage.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketTSIRMessage" %>
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
                    <div class="logheading">Filter : IC Ticket TSIR Message</div>
                    <div style="float: right; padding-top: 0px"> 
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
                                <asp:Label ID="Label6" runat="server" Text="Message Date From"></asp:Label>
                                <asp:TextBox ID="txtMessageFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtMessageFrom" PopupButtonID="txtMessageFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtMessageFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label9" runat="server" Text="Message Date To"></asp:Label>
                                <asp:TextBox ID="txtMessageTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtMessageTo" PopupButtonID="txtMessageTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtMessageTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
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
                                    <asp:GridView ID="gvTsir" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="TsirID" PageSize="20"  OnPageIndexChanging="gvTsir_PageIndexChanging">
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
                                            <asp:TemplateField HeaderText="Message">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTSIRMessage" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage.TSIRMessage")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Created By">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage.CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contact Number">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceChargeDesc" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage.CreatedBy.ContactNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           

                                            <asp:TemplateField HeaderText="Created On">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage.CreatedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                                   
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