<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ReassignSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.ReassignSupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <style>
        .ChatButton {
            position: fixed;
            right: 0;
            top: 183px;
            list-style: none;
        }

        .ChatDataDisplay {
            position: fixed;
            display: block;
            right: 50px;
        }

        .ChatDataNoneDisplay {
            position: fixed;
            display: none;
            right: 10px;
        }

        .TextAlignRight {
            text-align: right;
        }

        .Left {
            float: left;
        }

        .Right {
            float: right;
        }
    </style>
    <script>
        document.getElementById('lblMessage').scrollIntoView(true);
        function toggleColor() {
            if ($('#MainContent_Div1').attr('class') == "ChatDataNoneDisplay") {
                $('#MainContent_Div1').removeClass('ChatDataNoneDisplay')
                $('#MainContent_Div1').addClass('ChatDataDisplay')
            }
            else {
                $('#MainContent_Div1').removeClass('ChatDataDisplay')
                $('#MainContent_Div1').addClass('ChatDataNoneDisplay')
            }
        }
    </script>
      <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <%-- <div id="Chat" runat="server" class="ChatButton">

        <img src="Images/Char.png" alt="Quick Links" width="70" height="118" onclick="toggleColor()" />
    </div>--%>

    <div>
        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 16pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Ticket Header</span>
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
                </Columns>
            </asp:GridView>
        </div>


        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 16pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Ticket Details</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>
        <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">


            <asp:GridView ID="gvTicketItem" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
                <Columns>
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "ItemStatus.Status")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned To">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo.ContactName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned By">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
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
                            <asp:Label ID="lblTicketResolutionType" Text='<%# DataBinder.Eval(Container.DataItem, "ResolutionType.ResolutionType")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resolution">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblResolution" Text='<%# DataBinder.Eval(Container.DataItem, "Resolution")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Number">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TRNumber")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Moved">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketResolutionType" Text='<%# DataBinder.Eval(Container.DataItem, "TRClosed")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Moved On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTRClosedOn" Text='<%# DataBinder.Eval(Container.DataItem, "TRClosedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
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
                    <asp:Label ID="lblActualDuration" runat="server" Text="Worked (H)" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td> 
                <td>
                    <asp:TextBox ID="txtActualDuration" runat="server" CssClass="TextBox"></asp:TextBox></td>
                <td></td> 
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="lblAssignedTo" runat="server" Text="Assigned To" CssClass="label"></asp:Label><div style="color: red">*</div>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="TextBox" Width="200px"></asp:DropDownList></td>
                <%--<td>
                    <asp:CheckBox ID="cbAllEmp" runat="server" AutoPostBack="true" OnCheckedChanged="cbAllEmp_CheckedChanged" />
                </td>--%>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Support Type" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSupportType" runat="server" CssClass="TextBox">
                        <asp:ListItem>L1</asp:ListItem>
                        <asp:ListItem>L2</asp:ListItem>
                        <asp:ListItem>L3</asp:ListItem>
                    </asp:DropDownList></td>
           
           </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Assigner   Remark" CssClass="label"></asp:Label></td>
                <td colspan="5">
                    <asp:TextBox ID="txtAssignerRemark" runat="server" TextMode="MultiLine" Height="100px" Width="500px" Style="position: relative; top: -1px; left: 2px;" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td colspan="5">
                    <div style="height: 7px; background-color: white;">
                    </div>
                    <div style="float: left; width: 45%; height: 50px; background-color: white;">
                    </div>
                    <div style="float: right; width: 55%; height: 50px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnReassign" runat="server" Text="Reassign" CssClass="InputButton" OnClick="btnReassign_Click"  />
                    </div>
                </td>

            </tr>

        </table>
    </div>

</asp:Content>