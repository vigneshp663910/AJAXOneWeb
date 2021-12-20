<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="RequestSupportTicket.aspx.cs" Inherits="DealerManagementSystem.SupportTicket.RequestSupportTicket" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Scripts/styles.css" />

    <div class="container">
        <div class="col2">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" />
            <table style="padding-left: 20px;">
                <tr>
                    <td>
                        <table  >
                            <tr>
                                <td colspan="2">
                                    <div style="height: 20px;">
                                        <br />
                                        <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095;">New support ticket</span>


                                        <div style="height: 5px; background-color: #3665c2;"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table style="width: 750px;">
                            <tr>
                                <td style="width: 150px;">
                                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label>
                                    <div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTicketType" runat="server" Style="position: relative;" CssClass="TextBox"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Subject" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="400px" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketDescription" runat="server" Text="Ticket Note" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="200px" Width="600px" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Contact Name" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactName" runat="server" Height="100%" Style="position: relative;" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Mobile No" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobileNo" runat="server" Height="100%" Style="position: relative;" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <%--   <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Priority Level" CssClass="label"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPriorityLevel" runat="server" Style="position: relative;" CssClass="TextBox" Text="1"></asp:TextBox></td>
                    </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Attached File" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" Style="position: relative; top: 0px; left: 0px;" CssClass="TextBox" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltest" Text='<%# Eval("test") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbRemove" runat="server" OnClick="Remove_Click">
                                                        <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="height: 30px;">
                                    </div>
                                    <div style="float: left; width: 45%; height: 60px;">
                                    </div>
                                    <div style="float: right; width: 55%; height: 60px; font-size: 19px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" OnClick="btnSave_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <div style="display: none">
                            <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ticket ID">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbTicketNo" runat="server">
                                                <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="SubCategory">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory.SubCategory")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%><asp:TemplateField HeaderText="Priority Leave">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "PriorityLeave")%>' runat="server"></asp:Label>--%>
                                    <asp:TextBox ID="txtPriorityLeave" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "PriorityLevel")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticket Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobileNo" Text='<%# DataBinder.Eval(Container.DataItem, "MobileNo")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created On">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
