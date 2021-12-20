<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PaidServiceReportNew.aspx.cs" Inherits="DealerManagementSystem.ViewService.PaidServiceReportNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : PO Report </div>
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
                                                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" Width="250px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="ICTicket Number"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtICTicketNumber" runat="server" CssClass="input"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="ICTicket Date From :"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtICTicketDateF" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICTicketDateF" PopupButtonID="txtICTicketDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICTicketDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="ICTicket Date To"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtICTicketDateT" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICTicketDateT" PopupButtonID="txtICTicketDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICTicketDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="CustomerCode"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="input"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Service Status"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlServiceStatusID" runat="server" CssClass="TextBox">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Open</asp:ListItem>
                                                            <asp:ListItem Value="2">Technician assigned</asp:ListItem>
                                                            <asp:ListItem Value="3">SE Reached</asp:ListItem>
                                                            <asp:ListItem Value="4">Restored</asp:ListItem>
                                                            <asp:ListItem Value="7">Reopen</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Service Type"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlServiceTypeID" runat="server" CssClass="TextBox">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Paid</asp:ListItem>
                                                            <asp:ListItem Value="6">Others</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="right">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />

                                                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="updateProgress" runat="server">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                                <tr>
                                    <td>
                                        <span id="txnHistory1:refreshDataGroup">
                                            <div class="boxHead">
                                                <div class="logheading">
                                                    <div style="float: left">
                                                        <table>
                                                            <tr>
                                                                <td>PO Report</td>

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

                                                <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="2500px" AllowPaging="true" PageSize="20"
                                                    OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ICTicket Number" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ICTicket Date" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dealer Code" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dealer Name" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Code" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatus")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Service Type" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Peirod" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Peirod")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Technician" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Technician.ContactName")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="95px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_material_id" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.Material")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="170px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.MaterialDesc")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quotation Number" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.QuotationNumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Proforma Invoice Number" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.ProformaInvoiceNumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Number" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.InvoiceNumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Value Before Tax" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.ValueBeforeTax","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.Tax","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceItem.Total","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvICTickets" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }


    </script>

</asp:Content>