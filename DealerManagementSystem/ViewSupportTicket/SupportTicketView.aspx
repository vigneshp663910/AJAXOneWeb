<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SupportTicketView.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.SupportTicketView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        //document.getElementById('lblMessage').scrollIntoView(true);
        //function toggleColor() {
        //    if ($('#MainContent_Div1').attr('class') == "ChatDataNoneDisplay") {
        //        $('#MainContent_Div1').removeClass('ChatDataNoneDisplay')
        //        $('#MainContent_Div1').addClass('ChatDataDisplay')
        //    }
        //    else {
        //        $('#MainContent_Div1').removeClass('ChatDataDisplay')
        //        $('#MainContent_Div1').addClass('ChatDataNoneDisplay')
        //    }
        //}

        function SendMessage() {
            if (confirm("Are you sure you send to message ...?")) {
                return true;
            }
            return false;
        }
    </script>

    <%-- <div id="Chat" runat="server" class="ChatButton">

        <img src="Images/Char.png" alt="Quick Links" width="70" height="118" onclick="toggleColor()" />
    </div>--%>

    <div style="display: none">
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
                    <%--<asp:TemplateField HeaderText="Department">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.Department.DepartmentName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Age">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAge" Text='<%# DataBinder.Eval(Container.DataItem, "Age")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closed On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAge1" Text='<%# DataBinder.Eval(Container.DataItem, "ClosedOn")%>' runat="server"></asp:Label>
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
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <table style="padding-left: 20px;">
        <tr>
            <td colspan="5">
                <div>
                    <br />
                    <span style="font-size: 16pt; font-family: Arial; text-align: left; color: #3E4095;">Ticket History</span>
                    <div style="height: 5px; background-color: #3665c2;"></div>
                </div>
            </td>
        </tr>
    </table>
    <div id="Div2" runat="server">
        <table style="height: 100%; width: 900px; padding-left: 20px;">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
                        <Columns>


                            <asp:TemplateField HeaderText="Message">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" Text='<%# DataBinder.Eval(Container.  DataItem, "ID")%>' runat="server" Visible="false" />
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 200px">
                                                <asp:Label ID="Label3" Text='<%# DataBinder.Eval(Container.  DataItem, "Sender")%>' runat="server" /></td>
                                            <asp:Label ID="Label5" Text='<%# DataBinder.Eval(Container.  DataItem, "Sendertime")%>' runat="server" /></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 200px">
                                                <asp:Label ID="Label4" Text='<%# DataBinder.Eval(Container.  DataItem, "Receiver")%>' runat="server" />
                                                <asp:Label ID="Label6" Text='<%# DataBinder.Eval(Container.  DataItem, "ReceiverTime")%>' runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.  DataItem, "Message")%>' runat="server" class='<%# DataBinder.Eval(Container.  DataItem, "CssClass")%>' /></td>
                                            <td>
                                                <asp:ImageButton ID="ibDownload" runat="server" ImageUrl='<%# DataBinder.Eval(Container.  DataItem, "FileType")%>' Width="30px" OnClick="ibDownload_Click" Visible='<%# DataBinder.Eval(Container.  DataItem, "MessageVisible")%>' class='<%# DataBinder.Eval(Container.  DataItem, "CssClass")%>' />
                                                <asp:LinkButton ID="lnkDownload" Text='<%# DataBinder.Eval(Container.  DataItem, "FileName")%>' runat="server" OnClick="lnkDownload_Click" Visible='<%# DataBinder.Eval(Container.  DataItem, "MessageVisible")%>' class='<%# DataBinder.Eval(Container.  DataItem, "CssClass")%>' />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="Message" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" Checked="true" AutoPostBack="true" />
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="Upload File" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" AutoPostBack="true" />
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="label" Width="100%" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"
                        TextMode="MultiLine" MaxLength="15" TabIndex="1" Width="900px" Height="100px"></asp:TextBox>


                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Send" CssClass="InputButton" OnClick="btnSend_Click" />
                </td>
            </tr>

        </table>
    </div>
    <div id="Div1" runat="server">
        <table style="height: 100%; width: 900px; padding-left: 20px;">
            <tr>
                <td>
                    <asp:GridView ID="gvchar" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
                        <Columns>
                            <asp:TemplateField HeaderText="Message" HeaderStyle-Width="10px">
                                <ItemStyle VerticalAlign="Middle" BackColor="White" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" Text='<%# DataBinder.Eval(Container.  DataItem, "ID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.  DataItem, "Name")%>' runat="server" />
                                    <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.  DataItem, "Message")%>' runat="server" Visible='<%# DataBinder.Eval(Container.  DataItem, "MessageVisible")%>' />
                                    <div id="divFile" runat="server" class='<%# DataBinder.Eval(Container.  DataItem, "CssClass")%>' visible='<%# DataBinder.Eval(Container.  DataItem, "FileTypeVisible")%>'>
                                        <asp:ImageButton ID="ibDownload" runat="server" ImageUrl='<%# DataBinder.Eval(Container.  DataItem, "FileType")%>' Width="30px" OnClick="ibDownload_Click" />
                                        <asp:LinkButton ID="lnkDownload" Text='<%# DataBinder.Eval(Container.  DataItem, "Message")%>' runat="server" OnClick="lnkDownload_Click" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rbMessage" runat="server" Text="Message" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" Checked="true" AutoPostBack="true" />
                    <asp:RadioButton ID="rbMFile" runat="server" Text="Upload File" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" AutoPostBack="true" />
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server"
                        TextMode="MultiLine" MaxLength="15" TabIndex="1" Width="900px" Height="100px"></asp:TextBox>


                    <asp:FileUpload ID="FileUpload" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="InputButton" OnClick="btnSend_Click" />
                </td>
            </tr>
            <%-- <tr>
                        <td>
                            <asp:TextBox ID="txtMessage" runat="server"
                                TextMode="MultiLine" MaxLength="15" TabIndex="1" Width="900px" Height="100px" OnTextChanged="btnSend_Click" AutoPostBack="true"  Visible="false"></asp:TextBox>
                          
                         
                            <asp:FileUpload ID="FileUpload" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <asp:ImageButton ID="ibReply" runat="server" ImageUrl="~/Images/Reply.jpg" Width="50px" Height="42px" OnClick="ibReply_Click" ToolTip="Reply" />
                            <asp:ImageButton ID="ibFileUpload" runat="server" ImageUrl="~/Images/Attach.png"  Width="50px" Height="50px" OnClick="ibFileUpload_Click" ToolTip="Attach Files" />
                          <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="InputButton" OnClick="btnSend_Click" Visible="false" ToolTip ="Send Or Upload Files" />
                        </td>
                    </tr>--%>
        </table>
    </div>


    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSend" />  
            <asp:AsyncPostBackTrigger ControlID="btnProcessData" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px; display: none">
        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 16pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Ticket Approval Details </span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvApprover" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
            <Columns>

                <%-- <asp:TemplateField HeaderText="Approve Requested By">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblRequestedBy" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.ContactName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="RequestedOn">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblRequestedOn" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedOn")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--     <asp:TemplateField HeaderText="Approver Name">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblApprover" Text='<%# DataBinder.Eval(Container.DataItem, "Approver.ContactName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Approved On">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblApprovedOn" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedOn")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Appoved">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblIsAppoved" Text='<%# DataBinder.Eval(Container.DataItem, "IsAppoved")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approver Remark">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblApproverRemark" Text='<%# DataBinder.Eval(Container.DataItem, "ApproverRemark")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>