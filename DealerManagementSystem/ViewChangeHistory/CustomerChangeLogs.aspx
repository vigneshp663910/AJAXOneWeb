<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="CustomerChangeLogs.aspx.cs" Inherits="DealerManagementSystem.ViewChangeHistory.CustomerChangeLogs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">
                <div class="col-md-2 text-left">
                    <label>Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <label>Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <label>Customer Code</label>
                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>

    <div class="col-md-12" id="divList" runat="server">
        <asp:TabContainer ID="tabCustomerLogDetails" runat="server" ToolTip="Customer Log Details" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">

            <asp:TabPanel ID="tabCustomerLogs" runat="server" HeaderText="Customer" Font-Bold="True" ToolTip="Customer">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCustomerAttributeLogs" runat="server" HeaderText="Attribute" Font-Bold="True" ToolTip="Attribute">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerAttributeLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCustomerProductLogs" runat="server" HeaderText="Product" Font-Bold="True" ToolTip="Product">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerProductLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCustomerTeamLogs" runat="server" HeaderText="Team" Font-Bold="True" ToolTip="Team">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerTeamLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCustomerResponsibleEmployeeLogs" runat="server" HeaderText="Responsible Employee" Font-Bold="True" ToolTip="Responsible Employee">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerResponsibleEmployeeLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCustomerGroupOfCompaniesLogs" runat="server" HeaderText="Group Of Companies" Font-Bold="True" ToolTip="Group Of Companies">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerGroupOfCompaniesLogs" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCusSupDocLogs" runat="server" HeaderText="Support Documents" Font-Bold="True" ToolTip="Support Documents">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCusSupDoc" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCusShiptoParLogs" runat="server" HeaderText="Ship To Party" Font-Bold="True" ToolTip="Ship To Party">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCusShiptoPar" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                </ContentTemplate>
            </asp:TabPanel>




        </asp:TabContainer>
    </div>

</asp:Content>
