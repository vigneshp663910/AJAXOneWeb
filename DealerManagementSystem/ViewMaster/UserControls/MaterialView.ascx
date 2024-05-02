<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaterialView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.MaterialView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lnkBtnAddDrawing" runat="server" OnClick="lnkBtnActions_Click">Add Drawing</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div runat="server" class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Material View</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Material Code : </label>
                    <asp:Label ID="lblMaterialCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Material Type : </label>
                    <asp:Label ID="lblMaterialType" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model : </label>
                    <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Net Wt : </label>
                    <asp:Label ID="lblNetWt" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>HSN : </label>
                    <asp:Label ID="lblHSN" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>GST % : </label>
                    <asp:Label ID="lblGSTPer" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Material Desc : </label>
                    <asp:Label ID="lblMaterialDesc" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Division Code : </label>
                    <asp:Label ID="lblDivisionCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model Description : </label>
                    <asp:Label ID="lblModelDesc" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Wt Unit : </label>
                    <asp:Label ID="lblWtUnit" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>CST % : </label>
                    <asp:Label ID="lblCSTPer" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>UOM : </label>
                    <asp:Label ID="lblUOM" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mode Code : </label>
                    <asp:Label ID="lblModeCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gross Wt : </label>
                    <asp:Label ID="lblGrossWt" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Div : </label>
                    <asp:Label ID="lblDiv" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>SST % : </label>
                    <asp:Label ID="lblSSTPer" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:TabContainer ID="tbpPresalesMasters" runat="server" ToolTip="Presales Masters..." Font-Bold="True" Font-Size="Medium">
    <asp:TabPanel ID="tbPnlMaterial" runat="server" HeaderText="Drawing" Font-Bold="True" ToolTip="Drawing">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvMaterialDrawing" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("MaterialDrawingID") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblMaterialDrawingDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MaterialDrawingID")%>' OnClick="lblMaterialDrawingDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
<asp:Panel ID="pnlAddDrawing" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueAddDrawing">Add Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnAddDrawingClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAddDrawingMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUpload" runat="server" accept=".png,.jpg,.jpeg,.gif" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddDrawing" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddDrawing_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddDrawing" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddDrawing" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
