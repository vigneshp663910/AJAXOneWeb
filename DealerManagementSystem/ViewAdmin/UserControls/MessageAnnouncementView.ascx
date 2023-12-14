<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageAnnouncementView.ascx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserControls.MessageAnnouncementView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbSendMessage" runat="server" OnClick="lbActions_Click" Visible="false">Send Message</asp:LinkButton>
            </div>
        </div>
    </div>
</div>--%>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Message Header</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Notification Number : </label>
                <asp:Label ID="lblNotificationNumber" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Date : </label>
                <asp:Label ID="lblDate" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>From : </label>
                <asp:Label ID="lblCreatedBy" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Valid From : </label>
                <asp:Label ID="lblValidFrom" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Valid To : </label>
                <asp:Label ID="lblValidTo" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Mail Responce : </label>
                <asp:Label ID="lblMailResponce" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-12">
                <label>Message : </label>
                <asp:Label ID="lblMsg" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpMessage" runat="server" ToolTip="Message..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlMessage" runat="server" HeaderText="Message" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvMessageTo" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGMessageAnnouncementItemID" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementItemID")%>' runat="server" Visible="false"/>
                                    <asp:Label ID="lblGMessageAnnouncementHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementHeaderID")%>' runat="server" Visible="false"/>
                                    <asp:Label ID="lblAssignTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Read Status" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblReadStatus" Text='<%# (DataBinder.Eval(Container.DataItem, "ReadStatus").ToString()=="True")?"Yes":"No"%>' runat="server" />
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
    </asp1:TabPanel>
</asp1:TabContainer>