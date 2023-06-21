<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketMarginWarrantyReport.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketMarginWarrantyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var tablefixedWidthID = document.getElementById('tablefixedWidthID');
            var $width = $(window).width() - 28;
            //   alert($width);
            //    tablefixedWidthID.css("width", ($width + "px"));
            tablefixedWidthID.style.width = $width + "px";
            //  $('.tablefixedWidth').css("width", $width);
            // var $width
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageMarginWarrantyChangeReport" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Code</label>
                            <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" BorderColor="Silver" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">IC Ticket</label>
                            <asp:TextBox ID="txtICTicket" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Requested Date From</label>
                            <asp:TextBox ID="txtRequestedDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequestedDateFrom" PopupButtonID="txtRequestedDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtRequestedDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Requested Date To</label>
                            <asp:TextBox ID="txtRequestedDateTo" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRequestedDateTo" PopupButtonID="txtRequestedDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRequestedDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" BorderColor="Silver">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                <asp:ListItem Value="2">Rejected</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-center">
                            <%--<label class="modal-label">-</label>--%>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report" id="divMarginWarrantyChangeReport" runat="server">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="table-responsive">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Margin Warranty Change Request(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCountMarginWarrantyChange" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnMarginWarrantyChangeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnMarginWarrantyChangeArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnMarginWarrantyChangeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnMarginWarrantyChangeArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvMarginWarrantyChange" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid"
                            PageSize="10" AllowPaging="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requested Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICRequestedDate" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentModel")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current HMR">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentHMRValue" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentHMRValue")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblCurrentHMRDate" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentHMRDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Warranty Start Date">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWarrantyStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyStartDate", "{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Warranty Expiry Date">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDate", "{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceType" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType")%>' runat="server"></asp:Label><div style="display: none">
                                            <asp:Label ID="lblServiceTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTypeID")%>' runat="server"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus")%>' runat="server"></asp:Label><div style="display: none">
                                            <asp:Label ID="lblServiceStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatusID")%>' runat="server"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Margin Remarks" HeaderStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Margin Warranty Requested Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarginWarrantyRequestedDate" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedOn","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsApproved" Text='<%# DataBinder.Eval(Container.DataItem, "IsApproved")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
