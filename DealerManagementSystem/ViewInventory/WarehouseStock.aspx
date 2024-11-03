<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarehouseStock.aspx.cs" Inherits="DealerManagementSystem.ViewInventory.WarehouseStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Popup {
            width: 95%;
            height: 95%;
            top: 128px;
            left: 283px;
        }

            .Popup .model-scroll {
                height: 80vh;
                overflow: auto;
            }
    </style>
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
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
                    </div>      
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="BtnExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true"  Width="100px" OnClick="BtnExcel_Click"></asp:Button>
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
                                        <td>Warehouse Stock : </td>
                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                             <div style="float: right">
                                <table>
                                    <tr style="background-color:#ffffff">
                                        <td>Total Inventory Value :  </td>
                                        <td><asp:Label ID="lblTotalInventoryValue" runat="server" CssClass="label"></asp:Label></td> 
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvStock" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" PageSize="10" AllowPaging="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Dealer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"  Font-Size="12px"/>
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"  Font-Size="12px"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Office">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server" Font-Size="12px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server" Font-Size="12px" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server" Font-Size="12px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.Division.DivisionCode")%>' runat="server" Font-Size="12px" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bin">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBinName" Text='<%# DataBinder.Eval(Container.DataItem, "BinName")%>' runat="server" Font-Size="12px" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="On Order Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbOnOrderQty" Text='<%# DataBinder.Eval(Container.DataItem, "OnOrderQty","{0:0}" )%>' runat="server" OnClick="lblLinkButton_Click" />
                                  <%--  <asp:Label ID="lblOnOrderQty" Text='<%# DataBinder.Eval(Container.DataItem, "OnOrderQty","{0:0}" )%>' runat="server" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transit Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                     <asp:LinkButton ID="lbTransitQty" Text='<%# DataBinder.Eval(Container.DataItem, "TransitQty","{0:0}" )%>' runat="server" OnClick="lblLinkButton_Click" />
                                    
                                   <%-- <asp:Label ID="lblTransitQty" Text='<%# DataBinder.Eval(Container.DataItem, "TransitQty","{0:0}")%>' runat="server" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unrestricted Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnrestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "UnrestrictedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Restricted Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "RestrictedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blocked Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBlockedQty" Text='<%# DataBinder.Eval(Container.DataItem, "BlockedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reserved Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                     <asp:LinkButton ID="lbReservedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReservedQty","{0:0}" )%>' runat="server" OnClick="lblLinkButton_Click" />
                                     <%--<asp:Label ID="lblReservedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReservedQty","{0:0}")%>' runat="server" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPerUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PerUnitPrice","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
     <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>


    <asp:Panel ID="pnlLeadDetails" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:Button ID="btnExcelDetails" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />

            <div class="model-scroll">
                <asp:GridView ID="gvDetails" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                    EmptyDataText="No Data Found">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />

                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
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
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_LeadDetails" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLeadDetails" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


</asp:Content>
