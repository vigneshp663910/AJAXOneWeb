<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerBusinessExcellence.aspx.cs" Inherits="DealerManagementSystem.ViewBusinessExcellence.DealerBusinessExcellence" %>

<%@ Register Src="~/ViewBusinessExcellence/UserControls/ViewDealerBusinessExcellence.ascx" TagPrefix="UC" TagName="UC_ViewDealerBusinessExcellence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <%--  <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
        <asp:ListItem Text="Mango" Value="1" />
        <asp:ListItem Text="Apple" Value="2" />
        <asp:ListItem Text="Banana" Value="3" />
        <asp:ListItem Text="Guava" Value="4" />
        <asp:ListItem Text="Orange" Value="5" />
    </asp:ListBox>--%>

    <div class="col-md-12">

        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Region</label>
                        <asp:DropDownList ID="ddlRegionID" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Year</label>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Month</label>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"> 
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" />
                        <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        --%>
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
                                                <td>Business Excellence:</td>

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
                            <asp:GridView ID="gvDealerB" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerBusinessExcellenceID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBusinessExcellenceID")%>' runat="server" Visible="false" />

                                            <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Dealer" DataField="Dealer.DealerCode"></asp:BoundField>
                                    <asp:BoundField HeaderText="Dealer Name" DataField="Dealer.DealerName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status.Status"></asp:BoundField>

                                    <asp:TemplateField HeaderText="Requested">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestedBy" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblRequestedOn" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedOn")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Submitted">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmittedBy" Text='<%# DataBinder.Eval(Container.DataItem, "SubmittedBy.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblSubmittedOn" Text='<%# DataBinder.Eval(Container.DataItem, "SubmittedOn")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval L1">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovalL1By" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL1By.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="ApprovalL1On" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL1On")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval L2">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovalL2By" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL2By.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="ApprovalL2On" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL2On")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval L3">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovalL3By" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL3By.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="ApprovalL3On" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL3On")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval L4">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovalL4By" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL4By.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="ApprovalL4On" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovalL4On")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />

                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
                <%--</div>--%>
            </div>
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_ViewDealerBusinessExcellence ID="UC_ViewDealerBusinessExcellence" runat="server"></UC:UC_ViewDealerBusinessExcellence>
        </div>
    </div>

</asp:Content>
