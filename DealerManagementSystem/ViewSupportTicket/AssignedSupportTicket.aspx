<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="AssignedSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.AssignedSupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <script type="text/javascript">
        $(document).ready(function () {

            var asnQunatity = 0;
            var gvTickets = document.getElementById('MainContent_gvTickets');

            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) {

                    var lblTicketSeverity = document.getElementById('MainContent_gvTickets_lblTicketSeverity_' + i);
                    var lblTicketStatus = document.getElementById('MainContent_gvTickets_lblTicketStatus_' + i);
                    var lblCreatedOn = document.getElementById('MainContent_gvTickets_lblCreatedOn_' + i);

                    if (lblCreatedOn != null) {



                        var pattern = /(\d{2})\/(\d{2})\/(\d{4})/;

                        var dt = new Date(lblCreatedOn.innerText.replace(pattern, '$3-$2-$1'));
                        if (dt == "Invalid Date") {
                            var CreatedOn = lblCreatedOn.innerText.split('/');
                            dt = new Date(CreatedOn[2].split(' ')[0], CreatedOn[1] - 1, CreatedOn[0]);
                        }

                        var someDate = new Date();
                        // someDate.setDate(someDate.getDate() - 1);

                        if (lblTicketSeverity.innerHTML == "SEVERITY  1 -  Address The call within 4 hrs") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 1);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  2  -  Address the call with 2days") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 2);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  3  - Address the call with in 1 week") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 7);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  4  - Address the call with in 1 month") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 31);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                    }
                }
            }
        });
 </script>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />



    <div class="DivHeader">
        <table>
            <tr>
                <td colspan="8">
                    <div style="height: 40px; background-color: white;">
                        <br />
                        <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 10px">Assigned Ticket Form</span>

                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox" Style="position: relative;"></asp:TextBox></td>
                <td style="width: 30px"></td>
                <td>
                    <asp:Label ID="lblAssignedBy" runat="server" Text="Assigned By" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAssignedBy" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                <td style="width: 30px"></td>


                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                <td></td>
                <td>
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                <td></td>
                <td>
                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox"></asp:DropDownList></td>
            </tr>
            <tr>

                <td colspan="8">
                    <div style="background-color: white;">
                    </div>
                    <div style="float: left; width: 45%; height: 44px; background-color: white;">
                    </div>
                    <div style="float: right; width: 55%; height: 44px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />

                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <%--  <div style="height:auto; width:800px; overflow-x:scroll ; overflow-y: hidden; padding-bottom:10px;"> --%>
    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">
            <Columns>
                 <asp:TemplateField HeaderText="">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ibMessage" runat="server"  Width="30px" OnClick="ibMessage_Click" />    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ticket ID">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                       
                            <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                            <asp:Label ID="lblItemID" Text='<%# DataBinder.Eval(Container.DataItem, "TicketItem.ItemID")%>' runat="server" Visible="false" />
                      
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
                <%--   <asp:TemplateField HeaderText="Repeat">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblRepeat" Text='<%# DataBinder.Eval(Container.DataItem, "Repeat")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%><asp:TemplateField HeaderText="Severity">
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
              <%--  <asp:TemplateField HeaderText="Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                <%-- <asp:TemplateField HeaderText="Justification">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblJustification" Text='<%# DataBinder.Eval(Container.DataItem, "Justification")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%><asp:TemplateField HeaderText="Status">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned By">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "TicketItem.AssignedBy.ContactName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assigned On">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "TicketItem.AssignedOn")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="Expectation (H)">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblActualDuration" Text='<%# DataBinder.Eval(Container.DataItem, "TicketItem.ActualDuration")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                <asp:TemplateField>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button ID="btnInProgress" runat="server" Text="In Progress" CssClass="InputButton" OnClick="btnInProgress_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button ID="btnReassign" runat="server" Text="Reassign" CssClass="InputButton" OnClick="btnReassign_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>