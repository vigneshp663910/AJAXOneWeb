<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupportTicketView.ascx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.UserControls.SupportTicketView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnMessage" runat="server" OnClick="lbActions_Click">Message</asp:LinkButton>
                <asp:LinkButton ID="lbtnUploadFile" runat="server" OnClick="lbActions_Click">Upload File</asp:LinkButton>
                <asp:LinkButton ID="lbtnSendApproval" runat="server" OnClick="lbActions_Click">Request For Approval</asp:LinkButton>
                <asp:LinkButton ID="lbtnApprove" runat="server" OnClick="lbActions_Click">Approve</asp:LinkButton>
                <asp:LinkButton ID="lbtnAssignTo" runat="server" OnClick="lbActions_Click">Assign To</asp:LinkButton>
                <asp:LinkButton ID="lbtnInProgress" runat="server" OnClick="lbActions_Click">In Progress</asp:LinkButton>
                <asp:LinkButton ID="lbtnResolve" runat="server" OnClick="lbActions_Click">Resolve</asp:LinkButton>
                <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbActions_Click">Cancel</asp:LinkButton>
                <asp:LinkButton ID="lbtnForceclose" runat="server" OnClick="lbActions_Click">Force Close</asp:LinkButton>
                <asp:LinkButton ID="lbtnClose" runat="server" OnClick="lbActions_Click">Close</asp:LinkButton>
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
                    <label>Severity : </label>
                    <asp:Label ID="lblSeverity" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Description: </label>
                    <asp:Label ID="lblDescription" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>CreatedBy : </label>
                    <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Category : </label>
                    <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ticket Type : </label>
                    <asp:Label ID="lblTicketType" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Age : </label>
                    <asp:Label ID="lblAge" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>CreatedBy Contact : </label>
                    <asp:Label ID="lblCreatedByContactNumber" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Sub Category : </label>
                    <asp:Label ID="lblSubCategory" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Closed On : </label>
                    <asp:Label ID="lblClosedOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Created On : </label>
                    <asp:Label ID="lblCreatedOn" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<asp:TabContainer ID="tbpTaskView" runat="server" ToolTip="Assigned Status..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="10">
    <asp:TabPanel ID="tpnlAssigned" runat="server" HeaderText="Assigned" Font-Bold="True" ToolTip="Assigned Status...">
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
                            <asp:TemplateField HeaderText="AssignedTo">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AssignedTo Contact">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedToContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo.ContactNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
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
                            <asp:TemplateField HeaderText="AssignedBy">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AssignedBy Contact">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedByContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactNumber")%>' runat="server"></asp:Label>
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
    <asp:TabPanel ID="tpnlTicketHistory" runat="server" HeaderText="Ticket History" Font-Bold="True" ToolTip="Ticket Conversations...">
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
                <%--<div class="col-md-12">
                    <div class="col-md-2">
                        <asp:RadioButton ID="rbMessage" runat="server" Text="Message" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" Checked="true" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2">
                        <asp:RadioButton ID="rbMFile" runat="server" Text="Upload File" OnCheckedChanged="rbMessage_CheckedChanged" GroupName="File" AutoPostBack="true" />
                    </div>
                </div>--%>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlTicketApprovalDetails" runat="server" HeaderText="Ticket Approval Details" Font-Bold="True" ToolTip="Approval Status...">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvApprover" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Approver">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblApprover" Text='<%# DataBinder.Eval(Container.DataItem, "Approver.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approver Contact">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblApproverContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Approver.ContactNumber")%>' runat="server"></asp:Label>
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
                            <asp:TemplateField HeaderText="Approved On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedOn" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedBy" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested Contact">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedByContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.ContactNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedOn" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedOn")%>' runat="server"></asp:Label>
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
<asp:Panel ID="pnlConversation" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">
            <asp:Label ID="Label2" runat="server" Text="Message"></asp:Label></span><a href="#" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageConversation" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset5" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="1" Rows="5"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:FileUpload ID="FileUpload" runat="server" Visible="false" Height="35px" CssClass="TextBox form-control" />
                </div>
                <div class="col-md-12" id="divMailNotification" runat="server" visible="false">
                    <label>Mail Notification</label>
                    <asp:DropDownList ID="ddlMailNotification" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn Save" OnClick="btnSend_Click" Width="95px" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Conversation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlConversation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
<asp:Panel ID="pnlAssignTo" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">
            <asp:Label ID="Label3" runat="server" Text="Assign To"></asp:Label></span><a href="#" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageAssignTo" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div style="display: none">
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label9" runat="server" Text="Requested By" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtRequestedBy" runat="server" Enabled="false" CssClass="TextBox form-control" Visible="false" />
                        <asp:TextBox ID="txtRequestedOn" runat="server" CssClass="TextBox form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label10" runat="server" Text="Ticket Type" CssClass="label" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label11" runat="server" Text="Status" CssClass="label" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketDescription" runat="server" Text="Requester Remark" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtRequesterRemark" runat="server" TextMode="MultiLine" Height="100%" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label4" runat="server" Text="Category" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label5" runat="server" Text="Subcategory" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label6" runat="server" Text="Severity" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblAssignedTo" runat="server" Text="Assigned To" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label7" runat="server" Text="Attached File" CssClass="label"></asp:Label>
                    <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" CssClass="TextBox form-control" />

                    <asp:GridView ID="gvNewFileAttached" runat="server" Width="100%" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle BorderStyle="None" />
                                <ItemTemplate>
                                    <asp:Label ID="lbltest" Text='<%# Eval("FileName") %>' runat="server" />
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

                    <asp:GridView ID="gvFileAttached" runat="server" Width="100%" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None" CssClass="table table-bordered table-condensed Grid">
                        <Columns>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("text") %>' CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label8" runat="server" Text="Support Type" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="ddlSupportType" runat="server" CssClass="TextBox form-control">
                        <asp:ListItem>L1</asp:ListItem>
                        <asp:ListItem>L2</asp:ListItem>
                        <asp:ListItem>L3</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 col-sm-6">
                    <asp:Label ID="Label1" runat="server" Text="Assigner Remark" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtAssignerRemark" runat="server" TextMode="MultiLine" CssClass="TextBox form-control"></asp:TextBox>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="InputButton btn Save" OnClick="btnAssign_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AssignTo" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssignTo" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
<asp:Panel ID="pnlSendApproval" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">
            <asp:Label ID="Label19" runat="server" Text="Send for Approval"></asp:Label></span><a href="#" role="button">
                <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblSendApproval" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label17" runat="server" Text="Approver" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="ddlapprovar" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" CssClass="InputButton btn Save" Width="200px" OnClick="btnSendForApproval_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_SendApproval" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSendApproval" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
<asp:Panel ID="pnResolve" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">
            <asp:Label ID="Label14" runat="server" Text="Resolve"></asp:Label></span><a href="#" role="button">
                <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblResolve" runat="server" Text="" CssClass="message" Visible="false" />

    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset3" runat="server">
            <div class="col-md-12 col-sm-6">
                <asp:Label ID="lblEffort" runat="server" Text="Effort (H)" CssClass="label"></asp:Label>
                <span style="color: red">*</span>
                <asp:TextBox ID="txtEffort" runat="server" CssClass="TextBox form-control"></asp:TextBox>
            </div>
            <div class="col-md-12 col-sm-6">
                <asp:Label ID="lblResolutionType" runat="server" Text="Resolution Type" CssClass="label"></asp:Label>
                <asp:DropDownList ID="ddlResolutionType" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
            </div>
            <div class="col-md-12 col-sm-6">
                <asp:Label ID="lblResolution" runat="server" Text="Resolution" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12 col-sm-6">
                <asp:TextBox ID="txtResolution" runat="server" TextMode="MultiLine" CssClass="TextBox form-control"></asp:TextBox>
            </div>
            <div class="col-md-12 col-sm-6">
                <asp:Label ID="Label12" runat="server" Text="Attached File" CssClass="label"></asp:Label>
                <asp:FileUpload ID="fuResolve" runat="server" ClientIDMode="Static" onchange="this.form.submit()" CssClass="TextBox form-control" />
                <asp:GridView ID="gvResolveNewFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle BorderStyle="None" />
                            <ItemTemplate>
                                <asp:Label ID="lbltest" Text='<%# Eval("FileName") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remove">
                            <ItemStyle BorderStyle="None" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbResRemove" runat="server" OnClick="ResRemove_Click">
                                    <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-12 col-sm-6">
                <asp:Label ID="Label13" runat="server" Text="Support Type" CssClass="label"></asp:Label>
                <asp:DropDownList ID="ddlResSupportType" runat="server" CssClass="TextBox form-control">
                    <asp:ListItem>L1</asp:ListItem>
                    <asp:ListItem>L2</asp:ListItem>
                    <asp:ListItem>L3</asp:ListItem>
                </asp:DropDownList>
            </div>


            <%-- <div class="col-md-2 col-sm-6">
                    <asp:Label ID="lblPurpose" runat="server" Text="Purpose" CssClass="label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtPurpose" runat="server" CssClass="TextBox form-control"   Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-12 col-sm-6">
                    <asp:Label ID="lblNote" runat="server" Text="Note" CssClass="label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtNote" runat="server" CssClass="TextBox form-control" Width="500px" TextMode="MultiLine"  Visible="false"></asp:TextBox>
                </div>--%>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnResolve" runat="server" Text="Resolve" CssClass="InputButton btn Save" OnClick="btnResolve_Click" />
                <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton btn Save" OnClick="btnBack_Click" />--%>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Resolve" runat="server" TargetControlID="lnkMPE" PopupControlID="pnResolve" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
