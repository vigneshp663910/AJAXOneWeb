<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="QuotationChangeHistory.aspx.cs" Inherits="DealerManagementSystem.ViewChangeHistory.QuotationChangeHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label7" runat="server" Text="Date From "></asp:Label>--%>
                    <label>Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                  <%--  <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
               <%--     <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>--%>
                    <label>Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    <%--<asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-2 text-left">
                    <label>Refernece Quotation Number</label>
                    <asp:TextBox ID="txtRefQuotationNo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <label>Quotation Fields</label>
                    <%--<asp:TextBox ID="txtChgItem" runat="server" CssClass="form-control"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlQuotationField" runat="server" CssClass="form-control" />

                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearchQuotationChgHst" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearchQuotationChgHst_Click" OnClientClick="return dateValidation();" />
                    <asp:Button ID="btnExportExcelQuotationChgHst" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelQuotationChgHst_Click" Width="125px" />
                </div>
            </div>
        </fieldset>
    </div>
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
                                        <td>Quotation Change History:</td>
                                        <td>
                                            <asp:Label ID="lblRowCountQuotationChgHst" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnQuotationChgHstArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnQuotationChgHstArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnQuotationChgHstArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnQuotationChgHstArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvQuotationChgHst" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvQuotationChgHst_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
