<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PresalesMasters.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.PresalesMasters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp:TabContainer ID="tbpPresalesMasters" runat="server" ToolTip="Presales Masters..." Font-Bold="True" Font-Size="Medium">
            <asp:TabPanel ID="tpnlSourceOfEnquiry" runat="server" HeaderText="Source Of Enquiry" Font-Bold="True" ToolTip="Source Of Enquiry...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Header</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Lead Source</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlLeadSource" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSearchLeadSource" runat="server" CssClass="btn Search" Text="Search" OnClick="btnSearchLeadSource_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvLeadSource" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvLeadSource_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LeadSource" SortExpression="LeadSource">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeadSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source")%>' runat="server" />
                                                    <asp:Label ID="lblLeadSourceID" Text='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtLeadSource" runat="server" placeholder="Lead Source" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblLeadSourceEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' OnClick="lblLeadSourceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblLeadSourceDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' OnClick="lblLeadSourceDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddLeadSource" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddLeadSource_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tpnlTypeOfActivity" runat="server" HeaderText="Type Of Activity" Font-Bold="True" ToolTip="Type Of Activity...">
                <ContentTemplate>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
