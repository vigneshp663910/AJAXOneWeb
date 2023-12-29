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
<br />
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Message Header</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Notification No : </label>
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
                <label>Mail Response : </label>
                <asp:Label ID="lblMailResponce" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Subject : </label>
                <asp:Label ID="lblSubject" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-12">
                <%--<label>Message : </label>--%>
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
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Message(s):</td>

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
                    <asp:GridView ID="gvMessageTo" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvMessageTo_PageIndexChanging" ShowFooter="false">
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
                                    <asp:Label ID="lblGMessageAnnouncementItemID" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementItemID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblGMessageAnnouncementHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementHeaderID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblAssignTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" Text='<%# (DataBinder.Eval(Container.DataItem, "Dealer.CodeWithDisplayName")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" Text='<%# (DataBinder.Eval(Container.DataItem, "AssignTo.Department.DealerDepartment")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" Text='<%# (DataBinder.Eval(Container.DataItem, "AssignTo.Designation.DealerDesignation")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Read Status">
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