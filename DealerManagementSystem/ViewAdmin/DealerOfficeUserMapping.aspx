<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerOfficeUserMapping.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.DealerOfficeUserMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-left">
                            <label>Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged"/>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Office</label>
                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Designation</label>
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>IsEnabled</label>
                            <asp:DropDownList ID="ddlIsEnabled" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="2">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>IsActive</label>
                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="2">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" />
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
                                                <td>DealerOfficeUserMapping(s):</td>

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
                            <asp:GridView ID="gvDealerOfficeUserMapping" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "User.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "User.Department.DealerDepartment")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "User.Designation.DealerDesignation")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IsActive">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbIsActiveH" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="cbIsActiveH_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkIsActive" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' OnCheckedChanged="ChkIsActive_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:Button ID="BtnSave" runat="server" Text="Update" CssClass="btn Save" UseSubmitBehavior="true" OnClick="BtnSave_Click" Width="65px" Visible="false"/>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
