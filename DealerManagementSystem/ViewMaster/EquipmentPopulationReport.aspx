<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentPopulationReport.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.EquipmentPopulationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter : IC Ticket Manage</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-right">
                        <label>Dealer Code</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Equipment</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Customer</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Warranty Start</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtWarrantyStart" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtWarrantyStart" PopupButtonID="txtWarrantyStart" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtWarrantyStart" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Warranty End</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtWarrantyEnd" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtWarrantyEnd" PopupButtonID="txtWarrantyEnd" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtWarrantyEnd" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Approval" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <div>
                    <label>Equipment Population Report</label>
                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                </div>
                <asp:GridView ID="gvEquipment" runat="server" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="Black" BackColor="#d8d8d8" />
                    <HeaderStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
