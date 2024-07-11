<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="StockTransfer.aspx.cs" Inherits="DealerManagementSystem.ViewInventory.StockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmStockTranfer() {
            var x = confirm("Are you sure you want Stock Tranfer?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
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
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
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
                                        <td>Stock : </td>
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
                    <asp:GridView ID="gvStock" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Dealer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Office">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnrestrictedQty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnrestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "UnrestrictedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RestrictedQty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "RestrictedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BlockedQty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBlockedQty" Text='<%# DataBinder.Eval(Container.DataItem, "BlockedQty","{0:0}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="ReservedQty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblReservedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReservedQty")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnTransit" runat="server" Text="Transfer" CssClass="btn Back" Width="75px" Height="25px" OnClick="btnTransit_Click" />
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
    

    <asp:Panel ID="pnlStockTranfer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Stock Tranfer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button14" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageStockMove" runat="server" Text="" CssClass="message" />
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label>Unrestricted Qty : </label>
                        <asp:Label ID="lblUnrestrictedQty" runat="server" CssClass="LabelValue"></asp:Label>
                        <asp:Label ID="lblMaterialID" runat="server" CssClass="LabelValue" Visible="false"></asp:Label>
                        <asp:Label ID="lblDealerID" runat="server" CssClass="LabelValue" Visible="false"></asp:Label>
                        <asp:Label ID="lblOfficeID" runat="server" CssClass="LabelValue" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Restricted Qty : </label>
                        <asp:Label ID="lblRestrictedQty" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Blocked Qty : </label>
                        <asp:Label ID="lblBlockedQty" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <label class="modal-label">Material Transit Type</label>
                        <asp:DropDownList ID="ddlStockMovementType" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-4">
                        <label>Quantity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <label>Remark</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn Search" Text="Save" OnClick="btnSave_Click" OnClientClick="return ConfirmStockTranfer();"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_StockTranfer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlStockTranfer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
