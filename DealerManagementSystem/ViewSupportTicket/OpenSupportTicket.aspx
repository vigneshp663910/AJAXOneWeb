<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="OpenSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.OpenSupportTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            var availableEmp = EmpArray;
            $("#MainContent_txtRequestedUserID").autocomplete({
                source: availableEmp
            });
        });
    </script>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <asp:Panel ID="pnList" runat="server">
        <div class="DivHeader">
            <table style="width: 950px; margin-bottom: -12px">
                <tr>
                    <td colspan="8">
                        <div style="height: 40px; background-color: white;">
                            <br />
                            <span style="font-size: 14pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 10px">Open Task</span>

                            <div style="height: 5px; background-color: #0072c6;"></div>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        <asp:Label ID="Label2" runat="server" Text="Req Date From" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRequestedDateFrom" runat="server" Style="position: relative;" CssClass="TextBox" TextMode="Date" />
                    </td>
                    <td style="width: 30px"></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="To" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRequestedDateTo" runat="server" Style="position: relative;" CssClass="TextBox" TextMode="Date" />
                    </td>
                    <td style="width: 30px"></td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label4" runat="server" Text="Requested User ID" CssClass="label"></asp:Label></td>
                    <td>
                        <%--  <asp:TextBox ID="txtRequestedUserID" runat="server" Style="position: relative;" CssClass="TextBox" />--%>

                        <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="TextBox"></asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Ticket ID" CssClass="label"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTicketId" runat="server" Style="position: relative;" CssClass="TextBox" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server" Style="position: relative;" CssClass="TextBox"></asp:DropDownList>
                    </td>
                    <td></td>
                    <%--  <td>
                        <asp:Label ID="lblTicketType" runat="server" Text="Status" CssClass="label"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Style="position: relative;" CssClass="TextBox"></asp:DropDownList>
                    </td>--%>
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
        <%--<div style="height:auto; width:800px; overflow-x:scroll ; overflow-y: hidden; padding-bottom:10px;"> --%>
        <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
            <asp:RadioButton ID="rbAssign" runat="server" Text="Assign" GroupName="ss" CssClass="label" Checked="true" />
            <asp:RadioButton ID="rbSendForApproval" runat="server" Text="Send for Approval" GroupName="ss" CssClass="label" />
           <%-- <asp:RadioButton ID="rbResolve" runat="server" Text="Resolve" GroupName="ss" CssClass="label" />--%>
                  <asp:RadioButton ID="rbReject" runat="server" Text="Reject" GroupName="ss" CssClass="label" />
            <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ibMessage" runat="server" Width="30px" OnClick="ibMessage_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket ID">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbTicketNo" runat="server" OnClick="lbTicketNo_Click">
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
                    <asp:TemplateField HeaderText="Ticket Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
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
                    <asp:TemplateField HeaderText="Requested By">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Requested On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
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
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnView" runat="server" Visible="false">
        <%--   <div style="background-color: rgba(193, 193, 193, 0.42); width: 800px; margin-top: 2px; margin-bottom: 0px">--%>
        <table style="width: 800px; margin-bottom: -12px">
            <tr>
                <td colspan="5">
                    <div style="height: 60px; background-color: white;">
                        <br />
                        <span style="font-size: 20pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 10px">Send For Approval Form</span>

                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
                <td style="width: 30px"></td>
                <td>
                    <asp:Label ID="lblRequestedOn" runat="server" Text="Requested By" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Category" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
                <td></td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTicketDescription" runat="server" Text="Ticket Description" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="70px" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
                <td></td>


                <td>
                    <asp:Label ID="Label7" runat="server" Text="Attached File" CssClass="label"></asp:Label></td>
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
                    <asp:Label ID="Label6" runat="server" Text="Ticket Type" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTicketType" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
                <td></td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Approver" CssClass="label"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlapprovar" runat="server" Style="position: relative;" CssClass="TextBox"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div style="background-color: white;">
                    </div>
                    <div style="float: left; width: 25%; height: 45px; background-color: white;">
                    </div>
                    <div style="float: right; width: 75%; height: 45px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" CssClass="InputButton" OnClick="btnSendForApproval_Click" /><asp:Button ID="btnBack" runat="server" Text="Go Back" CssClass="InputButton" OnClick="btnBack_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <%-- </div>--%>
    </asp:Panel>

      <asp:Panel ID="pnlReject" runat="server" Visible="false"> 
        <table style="width: 800px; margin-bottom: -12px">
            <tr>
                <td colspan="5">
                    <div style="height: 60px; background-color: white;">
                        <br />
                        <span style="font-size: 20pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 10px">Reject Form</span>

                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Ticket No" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTicketNoReject" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox></td>
                <td style="width: 30px"></td> 
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Description" CssClass="label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTicketNoRejectRemark" runat="server" TextMode="MultiLine" Height="70px" CssClass="TextBox" ></asp:TextBox></td>
                <td></td> 
            </tr>
         
            <tr>
                <td colspan="5">
                    <div style="background-color: white;">
                    </div>
                    <div style="float: left; width: 25%; height: 45px; background-color: white;">
                    </div>
                    <div style="float: right; width: 75%; height: 45px; background-color: white; font-size: 19px;">
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="InputButton" OnClick="btnReject_Click" />
                        <asp:Button ID="btnRejectBack" runat="server" Text="Go Back" CssClass="InputButton" OnClick="btnRejectBack_Click" />
                    </div>
                </td>
            </tr>
        </table> 
    </asp:Panel>
</asp:Content>

