<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ModelTemplateDesign.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.ModelTemplateDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <div class="text-right">
            <asp:Button ID="Button2" runat="server" CssClass="btn Back" Text="Back" />
        </div>
        <div class="col-md-12" style="padding-top: 0px">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
                <div class="col-md-12 View">
                    <div class="col-md-6">
                        <label>Lead Number : </label>
                        <asp:Label ID="lblLeadNumber" runat="server" Text="bcsvdhj" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Lead Date : </label>
                        <asp:Label ID="lblLeadDate" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Category : </label>
                        <asp:Label ID="lblCategory" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Progress Status : </label>
                        <asp:Label ID="lblProgressStatus" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Qualification : </label>
                        <asp:Label ID="lblQualification" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Source : </label>
                        <asp:Label ID="lblSource" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Status : </label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <label>Type : </label>
                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="bcsvdhj"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <div style="float: right;">
                            <div class="dropdown">
                                <asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />
                                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton1</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton2" runat="server">LinkButton2</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3" runat="server">LinkButton3</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton4" runat="server">LinkButton4</asp:LinkButton>
                                    <a href="/ViewPreSale/Pre_Sales_Dashboard"><i class="fa fa-sun-o"></i>&nbsp;&nbsp;Pre-Sales</a>
                                    <a href="#"><i class="fa fa-sun-o"></i>&nbsp;&nbsp;Equipment Sales</a>
                                    <a href="#"><i class="fa fa-sun-o"></i>&nbsp;&nbsp;Parts Sales</a>
                                    <a href="#"><i class="fa fa-sun-o"></i>&nbsp;&nbsp;Equipment Service</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Segment</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name" SortExpression="Country" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                    <asp:TextBox ID="txtGCCountry" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                    <asp:TextBox ID="txtGCCountry1" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile Number" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Id" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGCCountry1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
