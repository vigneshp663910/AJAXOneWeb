<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketFSRManage.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketFSRManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DMS_U_AvailabilityOfOtherMachine.ascx" TagPrefix="UC" TagName="UC_AvailabilityOfOtherMachine" %>
<%@ Register Src="~/UserControls/DMS_ICTicketServiceCharges.ascx" TagPrefix="UC" TagName="DMS_ICTicketServiceCharges" %>
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
    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">FSR Manage</div>
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
                                <asp:Label ID="lblPlant" runat="server" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" Text="IC Ticket Date From "></asp:Label>
                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY" Enabled="True"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" Text="IC Ticket Date To"></asp:Label>
                                <asp:TextBox ID="txtICLoginDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY" Enabled="True"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                &nbsp;<asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" OnClick="btnExportExcel_Click" />
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
                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                <%--  <asp:TabContainer ID="tbpVendorReg" runat="server">
                        <asp:TabPanel ID="tbpnlFSRManage" runat="server" HeaderText="FSR Manage">
                            <ContentTemplate>--%>
                <asp:Panel ID="pnlFSRManage" runat="server">
                    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td><span id="txnHistory1:refreshDataGroup">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>FSR Manage</td>
                                                    <td>
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                                                    </td>
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
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                    <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="False" CssClass="TableGrid" AllowPaging="True" DataKeyNames="fsrID" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="IC Ticket">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketNumber")%>' runat="server" />
                                                    <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="62px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IC Ticket Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="92px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FSR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "FSRNumber")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="62px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FSR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "FSRDate","{0:d}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="92px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerCode" )%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerName" )%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="76px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="77px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServiceType.ServiceType")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="79px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label></div>                                                                
                                                </ItemTemplate>
                                                <HeaderStyle Width="147px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Acknowledged By Customer" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsAcknowledged" Text='<%# DataBinder.Eval(Container.DataItem, "IsAcknowledged")%>' runat="server"  Visible='<%# DataBinder.Eval(Container.DataItem, "IsAcknowledged")%>'></asp:Label></div>
                                                        <asp:Button ID="btnRequest" runat="server" Text="Request for Acknowledge" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnRequest_Click" Visible='<%# DataBinder.Eval(Container.DataItem, "_IsAcknowledged")%>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="147px" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Signature">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbUpdateSignature" runat="server" OnClick="lbUpdateSignature_Click">Update Signature </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="147px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FSR-PDF">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibPDF" runat="server" Width="20px" ImageUrl="~/FileFormat/Pdf_Icon.jpg" OnClick="ibPDF_Click" />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </span></td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

