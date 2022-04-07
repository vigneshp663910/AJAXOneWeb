<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Application.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp:TabContainer ID="tbpApplication" runat="server" ToolTip="Applications..." Font-Bold="True" Font-Size="Medium">
            <asp:TabPanel ID="tpnlMainApplication" runat="server" HeaderText="Main Application" Font-Bold="True" ToolTip="MainApplication...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvMainApplication" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvMainApplication_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Application" SortExpression="MainApplication">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMainApplication" Text='<%# DataBinder.Eval(Container.DataItem, "MainApplication")%>' runat="server" />
                                                    <asp:Label ID="lblSubModuleID" Text='<%# DataBinder.Eval(Container.DataItem, "MainApplicationID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMainApplication" runat="server" placeholder="Main Application" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblMainApplicationEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MainApplicationID")%>' OnClick="lblMainApplicationEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblMainApplicationDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MainApplicationID")%>' OnClick="lblMainApplicationDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddMainApplication" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddMainApplication_Click" Width="70px" Height="33px" />
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
            <asp:TabPanel ID="tpnlSubApplication" runat="server" HeaderText="Sub Application" Font-Bold="True" ToolTip="SubApplication...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>MainApplication</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlMainApplication" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainApplication_SelectedIndexChanged"></asp:DropDownList>
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
                                                        <td>Sub Application(s):</td>

                                                        <td>
                                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnSubAppArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnSubAppArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnSubAppArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnSubAppArrowRight_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:GridView ID="gvSubApplication" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvSubApplication_PageIndexChanging" OnDataBound="gvSubApplication_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Application" SortExpression="MainApplication">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMainApplication" Text='<%# DataBinder.Eval(Container.DataItem, "MainApplication.MainApplication")%>' runat="server" />
                                                    <asp:Label ID="lblMainApplicationID" Text='<%# DataBinder.Eval(Container.DataItem, "MainApplication.MainApplicationID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlGMainApplication" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Application" SortExpression="SubApplication">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubApplication" Text='<%# DataBinder.Eval(Container.DataItem, "SubApplication")%>' runat="server" />
                                                    <asp:Label ID="lblSubModuleID" Text='<%# DataBinder.Eval(Container.DataItem, "SubApplicationID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSubApplication" runat="server" placeholder="Sub Application" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblSubApplicationEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubApplicationID")%>' OnClick="lblSubApplicationEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblSubApplicationDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubApplicationID")%>' OnClick="lblSubApplicationDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddSubApplication" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddSubApplication_Click" Width="70px" Height="33px" />
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
        </asp:TabContainer>


    </div>
</asp:Content>
