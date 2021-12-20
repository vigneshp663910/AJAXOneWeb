<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyPartsAvailabilityReport.aspx.cs" Inherits="DealerManagementSystem.ServiceView.WarrantyPartsAvailabilityReport" %>
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
                    <div class="logheading">Filter</div>
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
                                <asp:Label ID="Label11" runat="server" Text="Material"></asp:Label>
                                <asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox>
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
                                                    <td>Warranty Parts Availability Report</td>
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
                                    <asp:GridView ID="gvICTicket" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true"  PageSize="20" OnPageIndexChanging="gvICTicket_PageIndexChanging">
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
                                               <asp:TemplateField HeaderText="Model">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceChargeCode" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quotation">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceChargeDesc" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quotation Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeOfWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationDate")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delivery">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available %">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAvailablePercentage" Text='<%# DataBinder.Eval(Container.DataItem, "AvailablePercentage")%>' runat="server"></asp:Label>
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