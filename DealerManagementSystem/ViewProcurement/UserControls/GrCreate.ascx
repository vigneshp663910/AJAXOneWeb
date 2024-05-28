<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GrCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.GrCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<fieldset class="fieldset-border" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
    <div class="col-md-12">
        <div class="col-md-12 col-sm-12">
            <label>Asn Number : </label>
            <asp:Label ID="lblAsnNumber" runat="server" CssClass="label"></asp:Label>
            <asp:Label ID="lblAsnID" runat="server" CssClass="label" Visible="false"></asp:Label>
        </div>
        <div class="col-md-12 col-sm-12">
            <label>Remarks</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <asp:GridView ID="gvPOAsnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Item">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAsnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItemID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblAsnID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asn Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Received Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReceivedQty")%>' runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtReceivedQty" runat="server" CssClass="form-control" TextMode="Number" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saleable Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblSaleableQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.SaleableQty")%>' runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtSaleableQty" runat="server" CssClass="form-control" TextMode="Number" Text='0'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Button ID="btnSetBlockedQty" runat="server" Text="Set Blocked Qty" CssClass="btn Save" Width="120px" Height="25px" OnClick="btnSetBlockedQty_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Blocked Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblBlockedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.BlockedQty")%>' runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtBlockedQty" runat="server" CssClass="form-control" TextMode="Number" Text='0' Enabled="false"></asp:TextBox>
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


    <asp:Panel ID="pnlUpdateBlockedQty" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Blocked Qty</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>

        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageBlockedQty" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border" id="Fieldset7" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Blocked Qty</legend>
                    <div class="col-md-12">
                        <div class="col-md-3 col-sm-12">
                            <label class="modal-label">Qty<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12">
                            <label class="modal-label">Remark<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12">
                            <label class="modal-label">Status<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn Save" OnClick="btnAdd_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_UpdateBlockedQty" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateBlockedQty" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
</fieldset>
