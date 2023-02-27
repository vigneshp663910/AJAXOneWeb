<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadFoloowUpReport.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.LeadFoloowUpReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Employee</label>
                        <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Number</label>
                        <asp:TextBox ID="txtLeadNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Date From</label>
                        <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Date To</label>
                        <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Qualification</label>
                        <asp:DropDownList ID="ddlSQualification" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Customer</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control"  AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>State</label>
                        <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Product Type</label>
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search"
                            UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" /> 
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
                                                <td>Equipment Population:</td>

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
                            <asp:GridView ID="gvEquipment" runat="server" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />

                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
    </div>
    </div>
</asp:Content>

