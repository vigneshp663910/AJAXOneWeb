<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="AssignSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.AssignSupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $('[data-url]').each(function () {
            var $this = $(this);
            $this.html('<a href="' + $this.attr('data-url') + '">' + $this.text() + '</a>');
        });
    </script>

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <table>
        <tr>
            <td colspan="5">
                <div>
                    <br />
                    <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Task Header</span>
                    <div style="height: 5px; background-color: #0072c6;"></div>
                </div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
            <Columns>
                <asp:TemplateField HeaderText="Ticket ID">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SubCategory">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory.SubCategory")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Repeat">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblRepeat" Text='<%# DataBinder.Eval(Container.DataItem, "Repeat")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Severity">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "Severity.Severity")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ticket Type">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created On">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.Department.DepartmentName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <table>
        <tr>
            <td colspan="5">
                <div>
                    <br />
                    <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Ticket Form</span>
                    <div style="height: 5px; background-color: #0072c6;"></div>
                </div>
            </td>
        </tr>
    </table>
    <div style="width: 800px; margin-top: 2px; margin-bottom: 0px">

        <table>
            <tr>

                <td>
                    <asp:Label ID="Label3" runat="server" Text="Requested By" CssClass="label" Visible="false"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtRequestedBy" runat="server" Enabled="false" Style="position: relative;" CssClass="TextBox" Visible="false" />
                    <asp:TextBox ID="txtRequestedOn" runat="server" CssClass="TextBox" Visible="false"></asp:TextBox>
                </td>
                <td style="width: 30px"></td>

                <td>
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox" Style="position: relative; top: 0px; left: 0px;" Enabled="false" Visible="false"></asp:TextBox></td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label" Visible="false"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox" Enabled="false" Visible="false"></asp:DropDownList></td>
                <td></td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label" Visible="false"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox" Enabled="false" Visible="false"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTicketDescription" runat="server" Text="Requester Remark" CssClass="label" Visible="false"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtRequesterRemark" runat="server" TextMode="MultiLine" Height="100%" Style="position: relative;" CssClass="TextBox" Enabled="false" Visible="false"></asp:TextBox>
                </td>
                <td></td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>
                <td style="width: 100px">
                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                <td></td>

                <td>
                    <asp:Label ID="lblAssignedTo" runat="server" Text="Assigned To" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td>
                <td> 
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="TextBox" Width="200px"></asp:DropDownList> 
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Attached File" CssClass="label"></asp:Label></td>
                <td>
                    <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" Style="position: relative; top: 0px; left: 0px;" CssClass="TextBox" />

                    <asp:GridView ID="gvNewFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
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
                                    <asp:LinkButton ID="lbRemove" runat="server">
                                        <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td></td>
                <td>
                    <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle BorderStyle="None" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("text") %>' CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Support Type" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSupportType" runat="server" CssClass="TextBox">
                        <asp:ListItem>L1</asp:ListItem>
                        <asp:ListItem>L2</asp:ListItem>
                        <asp:ListItem>L3</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Assigner   Remark" CssClass="label"></asp:Label></td>
                <td colspan="5">
                    <asp:TextBox ID="txtAssignerRemark" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="position: relative; top: -1px; left: 2px;" CssClass="TextBox"></asp:TextBox></td>
            </tr>
            <tr>

                <td colspan="5">
                    <div style="height: 7px; background-color: white;">
                    </div>
                    <div style="float: left; width: 45%; height: 50px; background-color: white;">
                    </div>
                    <div style="float: right; width: 55%; height: 50px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnSave" runat="server" Text="Assign" CssClass="InputButton" OnClick="btnSave_Click" />
                    </div>
                </td>

            </tr>

        </table>
    </div>
</asp:Content>
