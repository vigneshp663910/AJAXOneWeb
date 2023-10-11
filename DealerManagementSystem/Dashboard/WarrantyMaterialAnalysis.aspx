<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyMaterialAnalysis.aspx.cs" Inherits="DealerManagementSystem.Dashboard.WarrantyMaterialAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" Font-Bold="true" Font-Size="24px" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" Width="250px" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-8 text-left"> 
                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>


    <!-- Placeholder for dashboard -->
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12" id="Div1">
             <label class="modal-label">Warranty Material Availability  Analysis</label>  
                <asp:GridView ID="gvMaterialAnalysis" runat="server" CssClass="table table-bordered table-condensed Grid"
                    EmptyDataText="No data available!" Width="98%" ShowHeaderWhenEmpty="true">
                     <AlternatingRowStyle BackColor="#ffffff" />
    <FooterStyle ForeColor="White" />
    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>  
        </div> 
    </div>
</asp:Content>
