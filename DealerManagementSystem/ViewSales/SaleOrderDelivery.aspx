<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SaleOrderDelivery.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SaleOrderDelivery" %>

<%@ Register Src="~/ViewSales/UserControls/SalesOrderDeliveryView.ascx" TagPrefix="UC" TagName="UC_SalesOrderDeliveryView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Back {
            float: right;
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageSODelivery" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divSODeliveryList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /> </legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Office</label>
                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Number</label>
                        <asp:TextBox ID="txtDeliveryNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Number</label>
                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Sale Order Number</label>
                        <asp:TextBox ID="txtSaleOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer Code</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomer" WatermarkText="Customer" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Sale Order Type</label>
                        <asp:DropDownList ID="ddlSaleOrderType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Date From</label>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Date To</label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp:TextBoxWatermarkExtender>
                    </div>
                     <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Order Status</label>
                        <asp:DropDownList ID="ddlDeliveryStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearchSODelivery_Click" OnClientClick="return dateValidation();" Width="95px" />
                       <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>
                        <asp:Button ID="btnExportExcelDetails" runat="server" Text="Export SO Delivery Details" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelDetails_Click" Width="200px" />
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>SO Delivery Doc(s):</td>
                                                <td>
                                                    <asp:Label ID="lblRowCountSODelivery" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeftSODelivery" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftSODelivery_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRightSODelivery" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightSODelivery_Click" /></td>

                                                 <td>
                                                    <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." Width="23" Height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="float: right; overflow: auto;">
                                        <%--<div style="float :left">
                                             
                                        </div>--%>
                                        <div style="float: right">
                                            <img id="fs" alt="" src="../Images/NormalScreen.png" onclick="ScreenControl(2)" width="23" height="23" style="display: none;" />
                                            <img id="rs" alt="" src="../Images/FullScreen.jpg" onclick="ScreenControl(1)" width="23" height="23" style="display: block;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvSODelivery" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                OnPageIndexChanging="gvSODelivery_PageIndexChanging"
                                OnRowDataBound="gvSODelivery_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <%--<asp:Button ID="btnViewSODelivery" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewSODelivery_Click" Width="75px" Height="25px" />--%>
                                             <asp:ImageButton ID="btnViewSODelivery" ImageUrl="~/Images/Preview.png" runat="server" ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle" OnClick="btnViewSODelivery_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Invoice Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                             <asp:Label ID="lblInvoiceNumberNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblInvoiceDateDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Order Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                             <asp:Label ID="lblSaleOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.SaleOrderNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblSaleOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.SaleOrderDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Office">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDivision" runat="server" ImageUrl="~/Images/SpareParts.png" Width="25" Height="25"/>
                                            <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Division.DivisionCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Customer.CustomerCode")%>' runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Customer.CustomerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SO Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSOType" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.SaleOrderType.SaleOrderType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
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
        <div class="col-md-12" id="divSODeliveryDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton">
                    <asp:Button ID="btnViewSODeliveryBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnViewSODeliveryBack_Click" />
                </div>
            </div>
            <UC:UC_SalesOrderDeliveryView ID="UC_SalesOrderDeliveryView" runat="server"></UC:UC_SalesOrderDeliveryView>
        </div>
    </div>
</asp:Content>
