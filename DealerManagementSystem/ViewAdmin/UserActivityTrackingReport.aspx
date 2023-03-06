<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UserActivityTrackingReport.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserActivityTrackingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>User ID</label>
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Name</label>
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>IsLocked</label>
                        <asp:DropDownList ID="ddlIsLocked" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>IsEnabled</label>
                        <asp:DropDownList ID="ddlIsEnabled" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>AJAXOne</label>
                        <asp:DropDownList ID="ddlAJAXOne" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Department</label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Designation</label>
                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
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
                                        <td>User(s):</td>

                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnUserArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnUserArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnUserArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnUserArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" BorderStyle="None" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvUser_PageIndexChanging" OnSorting="gvUser_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExternalReferenceID" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ExternalReferenceID")%>'></asp:Label>
                                    <asp:TextBox ID="txtExternalReferenceID" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ExternalReferenceID")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Id">
                                <ItemStyle BorderStyle="None" Width="150px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lbUserID" runat="server">
                                        <asp:Label ID="lblUserName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:Label>
                                    </asp:LinkButton>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="350px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>'></asp:Label>
                                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mail">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="350px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMail" runat="server" CssClass="label">
                                             <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Mail")%>'><%# DataBinder.Eval(Container.DataItem, "Mail")%></a>
                                    </asp:Label>
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="right" BorderStyle="None" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactNumber" runat="server" CssClass="label"> 
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                    </asp:Label>
                                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Locked" ItemStyle-Width="80px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsLocked" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsLocked")%>' Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Enabled" ItemStyle-Width="80px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsEnabled" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsEnabled")%>' Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AJAX One" ItemStyle-Width="80px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbAjaxOne" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ajaxOne")%>' Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Technician" ItemStyle-Width="80px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsTechnician" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsTechnician")%>' Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Login">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLastLoginDate" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "LastLoginDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DaysSince" SortExpression="DaysSince">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDaysSince" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "DaysSince")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LoginCount" SortExpression="LoginCount">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLoginCount" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "LoginCount")%>'></asp:Label>
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
</asp:Content>
