<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SupportTicketView.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.SupportTicketView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .View {
            background: rgb(241 241 241) !important;
        }

        .action-btn .btnactions.sticky {
            right: 10px;
        }

        .Back {
            margin-top: 7px;
        }
        .action-btn .btn.Approval{
            margin-right: 110px;
        }
    </style>
    <script>
        function SendMessage() {
            if (confirm("Are you sure you send to message ...?")) {
                return true;
            }
            return false;
        }
    </script>
    <div class="col-md-12">
        <div class="col-md-12" id="divColdVisitView" runat="server">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" />
                </div>
            </div>
            <div class="col-md-12">
                <div class="action-btn">
                    <div class="" id="boxHere"></div>
                    <div class="dropdown btnactions" id="customerAction">
                        <div class="btn Approval">Actions</div>
                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                            <asp:LinkButton ID="lbtnEditQuotation" runat="server">Pls Add Info</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server">Pls Add Info</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server">Pls Add Info</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Ticket Header</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Ticket ID : </label>
                                <asp:Label ID="lblTicketID" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Repeat : </label>
                                <asp:Label ID="lblRepeat" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Description: </label>
                                <asp:Label ID="lblDescription" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Created On : </label>
                                <asp:Label ID="lblCreatedOn" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Category : </label>
                                <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Severity : </label>
                                <asp:Label ID="lblSeverity" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Status : </label>
                                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Age : </label>
                                <asp:Label ID="lblAge" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Sub Category : </label>
                                <asp:Label ID="lblSubCategory" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Ticket Type : </label>
                                <asp:Label ID="lblTicketType" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Created By : </label>
                                <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Closed On : </label>
                                <asp:Label ID="lblClosedOn" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </fieldset>

                <asp:TabContainer ID="tbpTaskView" runat="server" ToolTip="Assigned Status..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
                    <asp:TabPanel ID="tpnlAssigned" runat="server" HeaderText="Assigned" Font-Bold="True" ToolTip="">
                        <ContentTemplate>
                            <div class="col-md-12 Report">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvTicketItem" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
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
                                            <%--<asp:TemplateField HeaderText="TR Number">
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
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlTicketHistory" runat="server" HeaderText="Ticket History" Font-Bold="True" ToolTip="">
                        <ContentTemplate>
                            <div class="col-md-12" id="Div1" runat="server">
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvchar" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
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
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:RadioButton ID="rbMessage" runat="server" Text="Message" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" Checked="true" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:RadioButton ID="rbMFile" runat="server" Text="Upload File" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="15" TabIndex="1" Rows="5"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:FileUpload ID="FileUpload" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn Save" OnClick="btnSend_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlTicketApprovalDetails" runat="server" HeaderText="Ticket Approval Details" Font-Bold="True" ToolTip="">
                        <ContentTemplate>
                            <div class="col-md-12 Report">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvApprover" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestedOn">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestedOn" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </div>

        <%--<div class="col-md-12">
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Ticket Header</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvTickets" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
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
    </div>--%>
    </div>
</asp:Content>
