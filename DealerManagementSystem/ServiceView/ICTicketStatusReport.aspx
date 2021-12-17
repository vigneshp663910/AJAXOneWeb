<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketStatusReport.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketStatusReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <%--  <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
        <ContentTemplate>
          <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="position: fixed; top: 35%; right: 46%" Width="100px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress> --%>
    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter : IC Ticket Status Report</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" Text="Customer Code"></asp:Label>
                                <asp:TextBox ID="txtCustomerCode" runat="server" />
                            </div>



                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" Text="IC Ticket Date From "></asp:Label>
                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Ticket Date To"></asp:Label>
                                <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" />
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label11" runat="server" Text="Machine Serial Number"></asp:Label>
                                <asp:TextBox ID="txtMachineSerialNumber" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 28px;">
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

    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <span id="txnHistory1:refreshDataGroup">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>IC Ticket Manage</td>
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
                    <div class="InputButtonRight-contain">
                    </div>
                    <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="ICTicketID" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                            <Columns>

                                <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="62px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket Date" HeaderStyle-Width="92px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requested Date" HeaderStyle-Width="76px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedDate" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type" HeaderStyle-Width="79px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceType" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="75px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Status" HeaderStyle-Width="147px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason for Decline" HeaderStyle-Width="76px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDeclinedReason" Text='<%# DataBinder.Eval(Container.DataItem, "ReqDeclinedReason")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Problem Reported" HeaderStyle-Width="76px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblComplaintDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ComplaintDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Model" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial No" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentSerialNo")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="No Claim" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoClaim" Text='<%# DataBinder.Eval(Container.DataItem, "NoClaim")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Claim Reason" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoClaimReason" Text='<%# DataBinder.Eval(Container.DataItem, "NoClaimReason")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Day Left For Claim Creation" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDayLeftForClaimCreation" Text='<%# DataBinder.Eval(Container.DataItem, "DayLeftForClaimCreation")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice number" HeaderStyle-Width="50px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Number" HeaderStyle-Width="50px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Claim.InvoiceNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Claim.InvoiceDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Claim Status" HeaderStyle-Width="50px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Claim.ClaimStatus")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days Since Claim Creation" HeaderStyle-Width="77px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDaysSinceClaimCreation" Text='<%# DataBinder.Eval(Container.DataItem, "Claim.DaysSinceClaimCreation")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer" HeaderStyle-Width="50px">
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
                            </Columns>
                        </asp:GridView>
                    </div>
                </span>
            </td>
        </tr>
    </table>

    <%-- </ContentTemplate>
       <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvICTickets" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />

        </Triggers>

    </asp:UpdatePanel>--%>
</asp:Content>