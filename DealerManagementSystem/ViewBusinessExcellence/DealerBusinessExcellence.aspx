<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerBusinessExcellence.aspx.cs" Inherits="DealerManagementSystem.ViewBusinessExcellence.DealerBusinessExcellence" %>

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
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
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
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Unattended</asp:ListItem>
                            <asp:ListItem Value="6">In Progress</asp:ListItem>
                            <asp:ListItem Value="4">Converted To Lead</asp:ListItem>
                            <asp:ListItem Value="5">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" /> 
                       <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                   --%> </div>
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
                            <asp:GridView ID="gvDealerB" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="5" runat="server" ShowHeaderWhenEmpty="true"
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
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryID")%>' CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Enquiry">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnquiryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "EnquiryNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblEnquiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "EnquiryDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName"></asp:BoundField>
                                    <asp:BoundField HeaderText="PersonName" DataField="PersonName"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Mail")%>'><%# DataBinder.Eval(Container.DataItem, "Mail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="State" DataField="State.State"></asp:BoundField>
                                    <asp:BoundField HeaderText="District" DataField="District.District"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Product" DataField="Product"></asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
                                    <asp:BoundField HeaderText="Source" DataField="Source.Source"></asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status.Status"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Created">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
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
