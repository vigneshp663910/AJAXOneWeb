<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="StockLedger.aspx.cs" Inherits="DealerManagementSystem.ViewInventory.StockLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                </div>
                <div class="col-md-2 text-left">
                    <label>Dealer Office</label>
                    <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 text-left">
                    <label>Material</label>
                    <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp1:TextBoxWatermarkExtender>
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    <asp1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp1:TextBoxWatermarkExtender>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" Width="100px" />
                </div>
            </div>
        </fieldset>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Stock Ledger(s):</td>
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

                    <asp:GridView ID="gvStock" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        PageSize="10" AllowPaging="true" EmptyDataText="No Data Found">

                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>

