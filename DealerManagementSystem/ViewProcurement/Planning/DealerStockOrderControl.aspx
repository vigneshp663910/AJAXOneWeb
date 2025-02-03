<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerStockOrderControl.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.Planning.DealerStockOrderControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"/>
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" Width="95px" />
                            <asp:Button ID="btnCreate" runat="server" CssClass="btn Save" Text="Create" OnClick="btnCreate_Click" Width="65px" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Stock Order Control</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                             <td>
                                                <asp:Label ID="Label1" runat="server" Text="List" CssClass="label"></asp:Label></td>
                                            <td>
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
                        <asp:GridView ID="gvDealerStockOrderControl" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20"  EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15px" ItemStyle-BackColor="#039caf" ItemStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerStockOrderControlID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerStockOrderControlID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode") + " " + DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                        <%--<br />
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Count">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaxCount" Text='<%# DataBinder.Eval(Container.DataItem, "MaxCount")%>' runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minimum Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinimumValue" Text='<%# DataBinder.Eval(Container.DataItem, "MinimumValue")%>' runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Default Count">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefaultCount" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultCount")%>' runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Count Value" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountValue" Text='<%# DataBinder.Eval(Container.DataItem, "CountValue")%>' runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEdit" runat="server" OnClick="LnkEdit_Click" ToolTip="Edit"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="LnkDelete" runat="server" OnClick="LnkDelete_Click" ToolTip="Delete"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                        <asp:HiddenField ID="HidDealerStockOrderControlID" runat="server" />
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlCreateDealerSOControl" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueCreateDealerSOControl">Create Dealer Stock Order Control</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="PopupCreateDealerSOControlClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageCreateDealerSOControl" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Create Dealer Stock Order Control</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Dealer Code</label>
                            <asp:DropDownList ID="ddlCDealerCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Max Count</label>
                            <asp:TextBox ID="txtMaxCount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Minimum Value</label>
                            <asp:TextBox ID="txtMinimumValue" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12" runat="server" id="divDefaultvalue">
                            <label class="modal-label">Default Count</label>
                            <asp:TextBox ID="txtDefaultCount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSaveDealerSOControl" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveDealerSOControl_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_DealerSOControl" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCreateDealerSOControl" BackgroundCssClass="modalBackground" />
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
