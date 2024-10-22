<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerOfficeUserMapping.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.DealerOfficeUserMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" />
    <asp:TabContainer ID="tbpDealerOfficeUser" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0" AutoPostBack="true" OnActiveTabChanged="tbpDealerOfficeUser_ActiveTabChanged">
        <asp:TabPanel ID="tpnlDealerOfficeUserReport" runat="server" HeaderText="Dealer Office User Report" Font-Bold="True" ToolTip="Dealer Office User Report">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" id="divRpt" runat="server">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-left">
                                        <label>Dealer</label>
                                        <asp:DropDownList ID="ddlRDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRDealer_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label>Dealer Employee</label>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label>Office</label>
                                        <asp:DropDownList ID="ddlROffice" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label class="modal-label">Department</label>
                                        <asp:DropDownList ID="ddlRDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRDepartment_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label class="modal-label">Designation</label>
                                        <asp:DropDownList ID="ddlRDesignation" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnRSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnRSearch_Click" />
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn Search" OnClick="btnExcel_Click" />
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
                                                            <td>Dealer Office User List(s):</td>

                                                            <td>
                                                                <asp:Label ID="lblRRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnRArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnRArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnRArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnRArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="gvDealerOfficeUserReport" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="45px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UserID">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserID")%>' runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name">
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
                                                <asp:TemplateField HeaderText="Permission Given By">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActionGivenBy" Text='<%# DataBinder.Eval(Container.DataItem, "ActionGivenBy.ContactName")%>' runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblActionGivenOn" Text='<%# DataBinder.Eval(Container.DataItem, "ActionGivenOn")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Permission Modified By">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModifiedBy" Text='<%# DataBinder.Eval(Container.DataItem, "ModifiedBy.ContactName")%>' runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblModifiedOn" Text='<%# DataBinder.Eval(Container.DataItem, "ModifiedOn")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IsActive">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsActive" Text='<%# Eval("IsActive").ToString() == "True" ? "Yes" : "No"%>' runat="server"></asp:Label>
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
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpnlDealerOfficeUserMapping" runat="server" HeaderText="Dealer Office User Mapping" Font-Bold="True" ToolTip="Dealer Office User Mapping">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" id="divList" runat="server">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-left">
                                        <label>Dealer</label>
                                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
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
                                                            <td>Dealer Office User Mapping(s):</td>

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
                                                <asp:TemplateField HeaderText="UserID">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserID")%>' runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name">
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
                                        <asp:Button ID="BtnSave" runat="server" Text="Update" CssClass="btn Save" UseSubmitBehavior="true" OnClick="BtnSave_Click" Width="100px" Visible="false" />
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
