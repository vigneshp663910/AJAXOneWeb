<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ManageSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.ManageSupportTicket" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f"; ss
                                }
                            }
                        }
                    }
                }
            }
        });
    </script>
    <div class="container">
        <div class="col2">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table style="padding-left: 20px;">
                <tr>
                    <td colspan="2">
                         
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <div style="height: 20px;">
                                           
                                            <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095;">Ticket Status</span>


                                            <div style="height: 5px; background-color: #3665c2;"></div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                         
                         <br />
                        <table  >
                            <%-- <tr>
                    <td colspan="13">
                        <div>
                            <br />
                            <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095;">Ticket Status</span>

                            <div style="height: 5px; background-color: #3665c2;"></div>
                        </div>
                    </td>
                </tr>--%>

                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox"></asp:TextBox></td>
                                <td style="width: 30px"></td>
                                <td>
                                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                <td style="width: 30px"></td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:ListBox ID="lbStatus" runat="server" SelectionMode="Multiple" CssClass="TextBox" Height="50px"></asp:ListBox>
                                </td>

                                <td style="width: 30px"></td>
                                <td>
                                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                                <td style="width: 30px"></td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSave_Click" /></td>
                            </tr>


                        </table>
                    </td>
                </tr>
            </table>
            <br />
            
            <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
                <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Ticket ID">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ibMessage" runat="server" Width="30px" OnClick="ibMessage_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <asp:TemplateField HeaderText="Subject">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Repeat">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblRepeat" Text='<%# DataBinder.Eval(Container.DataItem, "Repeat")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%><%--  <asp:TemplateField HeaderText="Severity">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "Severity.Severity")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%><asp:TemplateField HeaderText="Ticket Type">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <%--   <asp:TemplateField HeaderText="Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
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
                        <asp:TemplateField HeaderText="Created On">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="InputButton" OnClick="btnClose_Click" />
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
                <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="InputButton" OnClick="btnExcel_Click" />
            </div>
        </div>
    </div>
</asp:Content>