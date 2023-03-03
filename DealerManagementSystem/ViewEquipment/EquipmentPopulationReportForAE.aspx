<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentPopulationReportForAE.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentPopulationReportForAE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />

    <div class="col-md-12">

        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">

                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Equipment</label>
                        <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Warranty Start</label>
                        <asp:TextBox ID="txtWarrantyStart" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtWarrantyStart" PopupButtonID="txtWarrantyStart" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtWarrantyStart" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Warranty End</label>
                        <asp:TextBox ID="txtWarrantyEnd" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="calendarextender4" runat="server" TargetControlID="txtWarrantyEnd" PopupButtonID="txtWarrantyEnd" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtWarrantyEnd" WatermarkText="DD/MM/YYYY" />
                    </div>

                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                    </div>
                     <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Warranty Status</label>
                        <asp:DropDownList ID="ddlWarrantyStatus" runat="server" CssClass="form-control" >
                            <asp:ListItem Value="2">All</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                         </asp:DropDownList>
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
