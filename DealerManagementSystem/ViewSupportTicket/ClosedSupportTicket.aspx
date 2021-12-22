<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ClosedSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.ClosedSupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div style="width: 871px; margin-top: -1px; margin-bottom: 0px; margin-left: auto; margin-right: auto">

        <table style="width: 800px; margin-bottom: -12px">
            <tr>
                <td colspan="5">
                    <div style="height: 60px; background-color: white;">
                        <br />
                        <span style="font-size: 20pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 10px">Closed Ticket Form</span>

                        
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
      
            <tr>
                <td>
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox" Style="position: relative;"></asp:TextBox></td>
                <td></td>
                <td>
                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>

                <td>
                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                <td></td>
               
            </tr>

            <tr>

                <td colspan="5">
                    <div style="height: 7px; background-color: white;">
                    </div>
                    <div style="float: left; width: 45%; height: 50px; background-color: white;">
                    </div>
                    <div style="float: right; width: 55%; height: 50px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />

                    </div>
                </td>

            </tr>
        </table>


    </div>

    <br />
    <br />

    <%-- <div style="height:auto; width:800px; overflow-x:scroll ; overflow-y: hidden; padding-bottom:10px;"> --%>
    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">


            <Columns>
                <asp:TemplateField HeaderText="Ticket ID">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbTicketNo" runat="server" OnClick="lbTicketNo_Click">
                            <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "TicketID")%>' runat="server" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "TicketCategory.TicketCategory")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SubCategory">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "TicketSubCategory.TicketSubCategory")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--  <asp:TemplateField HeaderText="Repeat">
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblRepeat" Text='<%# DataBinder.Eval(Container.DataItem, "Repeat")%>' runat="server"></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Severity">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "TicketSeverity.TicketSeverity")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ticket Type">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "TicketType.TicketType")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "TicketDescription")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               
                <asp:TemplateField HeaderText="Status">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "TicketStatus.TicketStatus")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Created By">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.EmployeeName")%>' runat="server"></asp:Label>
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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">
            <Columns>
                <asp:TemplateField HeaderText="Ticket ID">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbTicketNo" runat="server" OnClick="lbTicketNo_Click">
                            <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "TicketID")%>' runat="server" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "TicketDescription")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Justification">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblJustification" Text='<%# DataBinder.Eval(Container.DataItem, "Justification")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "TicketStatus.TicketStatus")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned To">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo.EmployeeName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned By">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.EmployeeName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned On">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expectation (H)">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblActualDuration" Text='<%# DataBinder.Eval(Container.DataItem, "ActualDuration")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Effort (H)">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Resolution Type">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketResolutionType" Text='<%# DataBinder.Eval(Container.DataItem, "TicketResolutionType.TicketResolutionType")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Resolution">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblResolution" Text='<%# DataBinder.Eval(Container.DataItem, "Resolution")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>