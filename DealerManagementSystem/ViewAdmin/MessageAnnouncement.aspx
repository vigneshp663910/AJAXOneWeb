<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MessageAnnouncement.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.MessageAnnouncement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewAdmin/UserControls/MessageAnnouncementCreate.ascx" TagPrefix="UC" TagName="UC_MessageAnnouncementCreate" %>
<%@ Register Src="~/ViewAdmin/UserControls/MessageAnnouncementView.ascx" TagPrefix="UC" TagName="UC_MessageAnnouncementView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .back-buttton #MainContent_btnViewBackToList {
            float: right;
            margin-right: 20px;
            margin-top: -6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12" id="DivMessageHeader" runat="server">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-left">
                                <label>Dealer</label>
                                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label>Department</label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label>Designation</label>
                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label>Employee</label>
                                <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 col-sm-12" id="ChkMessage" runat="server">
                                <br />
                                <asp:CheckBox ID="ChkGetAllMessage" runat="server" Text="Get All Message" />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnMessage" runat="server" Text="Message" CssClass="btn Save" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnMessage_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>


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
                            <asp:GridView ID="gvMessageAnnouncement" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkViewMessage" runat="server" OnClick="btnViewMessage_Click" ToolTip="View"><i class="fa fa-fw fa-play" style="font-size:13px"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LnkForwardMessage" runat="server" OnClick="LnkForwardMessage_Click" ToolTip="Forward"><i class="fa fa-fw fa-forward" style="font-size:13px"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LnkDraftEdit" runat="server" OnClick="LnkDraftEdit_Click" ToolTip="Draft Edit"><i class="fa fa-fw fa-edit" style="font-size:13px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notification No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotificationNo" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementHeaderID")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valid From">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblValidFrom" Text='<%# DataBinder.Eval(Container.DataItem, "ValidFrom")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valid To">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblValidTo" Text='<%# DataBinder.Eval(Container.DataItem, "ValidTo")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created On">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemStyle VerticalAlign="Middle"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Message">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMessage" Text='<%# DataBinder.Eval(Container.DataItem, "Message")%>' runat="server" />
                                            <asp:Label ID="lblMessageAnnouncementId" Text='<%# DataBinder.Eval(Container.DataItem, "MessageAnnouncementHeaderID")%>' runat="server" Visible="false" />
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
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divMessageAnnouncementCreate" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server">
                <UC:UC_MessageAnnouncementCreate ID="UC_MessageAnnouncementCreate" runat="server"></UC:UC_MessageAnnouncementCreate>
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                <div class="col-md-12 text-center">
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divMessageAnnouncementView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnViewBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnViewBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server">
                <UC:UC_MessageAnnouncementView ID="UC_MessageAnnouncementView" runat="server"></UC:UC_MessageAnnouncementView>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                <div class="col-md-12 text-center">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
