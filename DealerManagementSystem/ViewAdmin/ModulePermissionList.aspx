<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ModulePermissionList.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.ModulePermissionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-left">
                            <label>Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Designation</label>
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Main Module</label>
                            <asp:DropDownList ID="ddlMainModule" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainModule_SelectedIndexChanged"/>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Sub Module<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlSubModule" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubModule_SelectedIndexChanged"/>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Child Module</label>
                            <asp:DropDownList ID="ddlChildModule" runat="server" CssClass="form-control"/>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="BtnSearch_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Dealer Operator(s):</td>
                                            <td><asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td><asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                            <td><asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvDealerUsers" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" OnPageIndexChanging="gvDealerUsers_PageIndexChanging" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="User Name" DataField="UserName"></asp:BoundField>
                                <asp:BoundField HeaderText="Contact Name" DataField="ContactName"></asp:BoundField>
                                <asp:BoundField HeaderText="Email" DataField="MailID"></asp:BoundField>
                                <asp:BoundField HeaderText="Department" DataField="DealerDepartment"></asp:BoundField>
                                <asp:BoundField HeaderText="Designation" DataField="DealerDesignation"></asp:BoundField>
                                <asp:BoundField HeaderText="Module Name" DataField="ModuleName"></asp:BoundField>
                                <asp:BoundField HeaderText="SubModule Name" DataField="SubModuleName"></asp:BoundField>
                                <asp:BoundField HeaderText="ChildModule Name" DataField="ChildName"></asp:BoundField>
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
