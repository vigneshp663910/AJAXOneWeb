<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewPhysicalInventoryPosting.ascx.cs" Inherits="DealerManagementSystem.ViewInventory.UserControls.ViewPhysicalInventoryPosting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<br />
<br />
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Enquiry Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer Code : </label>
                    <asp:Label ID="lblDealerCode" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer Name : </label>
                    <asp:Label ID="lblDealerName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Office Name : </label>
                    <asp:Label ID="lblOfficeName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Document Number : </label>
                    <asp:Label ID="lblDocumentNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Document Date : </label>
                    <asp:Label ID="lblDocumentDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Posting Date : </label>
                    <asp:Label ID="lblPostingDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
               
            </div>
            <div class="col-md-4">
                 <div class="col-md-12">
                    <label>Inventory Posting Type : </label>
                    <asp:Label ID="lblInventoryPostingType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Created By : </label>
                    <asp:Label ID="lblCreatedByContactName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>

        </div>
    </fieldset>
</div>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpEnquiry" runat="server" ToolTip="Enquiry Info..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="2">
    <asp1:TabPanel ID="tpnlStatusHistory" runat="server" HeaderText="Stock List" Font-Bold="True">
        <ContentTemplate>
            <asp:GridView ID="gvStatusHistory" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false"
                EmptyDataText="No Data Found">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex+1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Material" DataField="Material.MaterialCode" />
                    <asp:BoundField HeaderText="Material Description" DataField="Material.MaterialDescription" />
                    <asp:BoundField HeaderText="System Stock" DataField="SystemStock" />
                    <asp:BoundField HeaderText="Physical Stock" DataField="PhysicalStock" />
                    <asp:BoundField HeaderText="Is Posted" DataField="IsPosted" />
                </Columns>
                <AlternatingRowStyle BackColor="#ffffff" />
                <FooterStyle ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>
