<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceReport.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesInvoiceReport" %>

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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
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
                        <label class="modal-label">Customer Code</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomer" WatermarkText="Customer" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Number</label>
                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Date From</label>
                        <asp:TextBox ID="txtInvoiceDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" PopupButtonID="txtInvoiceDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtInvoiceDateFrom" WatermarkText="Date From"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Date To</label>
                        <asp:TextBox ID="txtInvoiceDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDateTo" PopupButtonID="txtInvoiceDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtInvoiceDateTo" WatermarkText="Date To"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Number</label>
                        <asp:TextBox ID="txtDeliveryNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Sale Order Number</label>
                        <asp:TextBox ID="txtSaleOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Sale Order Type</label>
                        <asp:DropDownList ID="ddlSaleOrderType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Order Status</label>
                        <asp:DropDownList ID="ddlDeliveryStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                        <%--<asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" Width="100px" OnClick="btnExportExcel_Click" />--%>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Sale Order Invoice : </td>
                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                        </td>
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
                    <asp:GridView ID="gvSOInvoice" runat="server" CssClass="table table-bordered table-condensed Grid"
                        AllowPaging="true" PageSize="20" EmptyDataText="No Data Found" AutoGenerateColumns="false">
                        <columns>
                            
                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15px">
                                <itemtemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                </itemtemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PDF">
                                <itemtemplate>
                                    <asp:Label ID="lblDeliveryByID" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server" Visible="false" />
                                    <asp:ImageButton ID="ibPDF" runat="server" Width="20px" ImageUrl="../Images/pdf_dload.png" OnClick="ibPDF_Click" Style="height: 50px; width: 60px;" />
                                </itemtemplate>
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Invoice Number">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice Number")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblInvoiceDateDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice Date","{0:d}")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Number">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery Number")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery Date","{0:d}")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sale Order Number">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:Label ID="lblSaleOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Sale Order Number")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblSaleOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "Sale Order Date","{0:d}")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dealer">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Code")%>' runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Name")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Office">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblDealerOffice" Text='<%# DataBinder.Eval(Container.DataItem, "Office Name")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "Division")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer Code")%>' runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer Name")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SO Type">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblSOType" Text='<%# DataBinder.Eval(Container.DataItem, "Order Type")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Status">
                                <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                <itemtemplate>
                                    <asp:Label ID="lblDeliveryOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gross Amount">
                                <itemstyle verticalalign="Middle" horizontalalign="Right" />
                                <itemtemplate>
                                    <asp:Label ID="lblGross" Text='<%# DataBinder.Eval(Container.DataItem, "Gross Amount")%>' runat="server"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                        </columns>
                        <alternatingrowstyle backcolor="#ffffff" />
                        <footerstyle forecolor="White" />
                        <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                        <pagerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                        <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
