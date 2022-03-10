<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PresalesMastersCompetatiorData.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.PresalesMastersCompetatiorData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp:TabContainer ID="tbConCompetratiorProduct" runat="server" ToolTip="Competratior Product" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
            <asp:TabPanel ID="tbPnlMake" runat="server" HeaderText="Make" Font-Bold="True" ToolTip="Make">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvMake" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvMake_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMake" runat="server" placeholder="Make" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkMakeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MakeID")%>' OnClick="lnkBtnMakeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkMakeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MakeID")%>' OnClick="lnkBtnMakeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateMake" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateMake_Click" Width="70px" Height="33px" />
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
                </contenttemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbPnlProductType" runat="server" HeaderText="Product Type" Font-Bold="True" ToolTip="Product Type">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvProductType" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" AllowPaging="True" ShowFooter="True" OnPageIndexChanging="gvProductType_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Type" SortExpression="ProductType">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductType" runat="server" placeholder="Product Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnProductTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' OnClick="lnkBtnProductTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnProductTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' OnClick="lnkBtnProductTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateProductType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateProductType_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </contenttemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbPnlProduct" runat="server" HeaderText="Product" Font-Bold="True" ToolTip="Product">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvProduct_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" SortExpression="Product">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProduct" runat="server" placeholder="Product" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnProductEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' OnClick="lnkBtnProductEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnProductDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' OnClick="lnkBtnProductDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateProduct" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateProduct_Click" Width="70px" Height="33px" />
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
                </contenttemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
