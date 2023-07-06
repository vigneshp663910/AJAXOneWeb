<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentClient.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Client</label>
                        <asp:TextBox ID="txtSClient" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearchEquipmentClient" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearchEquipmentClient_Click"></asp:Button>
                    </div>
                </div>
            </fieldset>
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
                                            <td>Client(s):</td>

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

                        <asp:GridView ID="gvClient" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvClient_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClient" Text='<%# DataBinder.Eval(Container.DataItem, "Client")%>' runat="server" />
                                        <asp:Label ID="lblEquipmentClientID" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentClientID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtClient" runat="server" placeholder="Client" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblClientEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EquipmentClientID")%>' OnClick="lblClientEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lblClientDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EquipmentClientID")%>' OnClick="lblClientDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="BtnAddClient" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddClient_Click" Width="70px" Height="33px" />
                                    </FooterTemplate>
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
</asp:Content>
