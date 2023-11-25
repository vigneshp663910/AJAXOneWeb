<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageAnnouncementCreate.ascx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserControls.MessageAnnouncementCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbSendMessage" runat="server" OnClick="lbActions_Click" Visible="false">Send Message</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Select Mail To Send Message</legend>
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <div class="col-md-3 col-sm-12">
                    <label>Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-3 col-sm-12">
                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-3 col-sm-12">
                    <label>Designation</label>
                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-3 col-sm-12">
                    <label class="modal-label">Employee</label>
                    <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealerEmployee_SelectedIndexChanged" AutoPostBack="true"/>
                </div>
            </fieldset>
            <div class="col-md-12 col-sm-12">
                <label class="modal-label">Message</label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </div>
            <div class="col-md-12 Report">
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvEmp_PageIndexChanging" OnSorting="gvEmp_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.CodeWithDisplayName")%>'></asp:Label>
                                    <asp:Label ID="lblDealerID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DID")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartmentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.Department.DealerDepartment")%>'></asp:Label>
                                    <asp:Label ID="lblDealerDepartmentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.Department.DealerDepartmentID")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.Designation.DealerDesignation")%>'></asp:Label>
                                    <asp:Label ID="lblDealerDesignationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.Designation.DealerDesignationID")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DealerEmployee" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerEmployee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.ContactName")%>'></asp:Label>
                                    <asp:Label ID="lblDealerEmployeeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.UserID")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="50px" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="10px" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChkMailH" Text="Check" runat="server" AutoPostBack="true" OnCheckedChanged="ChkMailH_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkMail" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "MailResponce")%>' AutoPostBack="true" OnCheckedChanged="ChkMail_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail ID" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignTo.Mail")%>'></asp:Label>
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
        </div>
    </fieldset>
</div>
