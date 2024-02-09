<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.LeadView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<%@ Register Src="~/ViewPreSale/UserControls/AssignSE.ascx" TagPrefix="UC" TagName="UC_AssignSE" %>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddCustomerConversation.ascx" TagPrefix="UC" TagName="UC_CustomerConversation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFinancial.ascx" TagPrefix="UC" TagName="UC_Financial" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLeadProduct.ascx" TagPrefix="UC" TagName="UC_Product" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddQuotation.ascx" TagPrefix="UC" TagName="UC_Quotation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnEditLead" runat="server" OnClick="lbActions_Click">Edit Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditExpectedDate" runat="server" OnClick="lbActions_Click">Edit Expected Date</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditNextFollowUpDate" runat="server" OnClick="lbActions_Click">Edit Next Follow-Up Date</asp:LinkButton>
                <asp:LinkButton ID="lbtnAssign" runat="server" OnClick="lbActions_Click">Assign</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFollowUp" runat="server" OnClick="lbActions_Click">Add Follow-up</asp:LinkButton>
                <%-- <asp:LinkButton ID="lbtnCustomerConversation" runat="server" OnClick="lbActions_Click">Customer Conversation</asp:LinkButton>--%>
                <%-- <asp:LinkButton ID="lbtnAddFinancialInfo" runat="server" OnClick="lbActions_Click">Financial Info</asp:LinkButton>--%>
                <asp:LinkButton ID="lbtnAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                <asp:LinkButton ID="lbtAddQuestionaries" runat="server" OnClick="lbActions_Click">Add Questionaries</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddVisit" runat="server" OnClick="lbActions_Click">Add Visit</asp:LinkButton>
                <asp:LinkButton ID="lbtnLostLead" runat="server" OnClick="lbActions_Click">Lost Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnCancelLead" runat="server" OnClick="lbActions_Click">Cancel Lead</asp:LinkButton>


                <%-- <asp:LinkButton ID="lbtnAddEffort" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddExpense" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>--%>
                <%--<asp:LinkButton ID="lbtnAddQuotation" runat="server" OnClick="lbActions_Click">Convert to Quotation</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>
<%-- <asp:LinkButton ID="lbtnConvertToProspect" runat="server" OnClick="lbActions_Click">Convert to Prospect</asp:LinkButton> --%>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Lead Number : </label>
                <asp:Label ID="lblLeadNumber" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Lead Date : </label>
                <asp:Label ID="lblLeadDate" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <%--  <div class="col-md-4">
                <label>Category : </label>
                <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
            </div>--%>
            <div class="col-md-4">
                <label>Expected Date of Sale : </label>
                <asp:Label ID="lblExpectedDateOfSale" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Application : </label>
                <asp:Label ID="lblApplication" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Qualification : </label>
                <asp:Label ID="lblQualification" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Source : </label>
                <asp:Label ID="lblSource" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Project : </label>
                <asp:Label ID="lblProject" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer Feed back : </label>
                <asp:Label ID="lblCustomerFeedback" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Remarks : </label>
                <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
            </div>

            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Contact Person : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Mobile : </label>
                <%-- <a class="care text-white" href="tel:+91 08067200014">+91 08067200014</a>--%>
                <asp:Label ID="lblMobile" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Email : </label>
                <asp:Label ID="lblEmail" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Address : </label>
                <asp:Label ID="lblLocation" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Financial Info : </label>
                <asp:Label ID="lblFinancialInfo" runat="server" CssClass="LabelValue"></asp:Label>
            </div>

            <%-- <div class="col-md-4">
                <label>Total Effort : </label>
                <asp:Label ID="lblTotalEffort" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Total Expense : </label>
                <asp:Label ID="lblTotalExpense" runat="server" CssClass="label"></asp:Label>
            </div>--%>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlSalesEngineer" runat="server" HeaderText="Sales Engineer" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvSalesEngineer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned On" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned By" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblActive" Text='<%# DataBinder.Eval(Container.DataItem, "Active")%>' runat="server" />
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
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabVisit" runat="server" HeaderText="Visit" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <%--  <div class="col-md-12">--%>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvVisit" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold Visit No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColdVisitID" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblColdVisitNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitNumber")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold Visit Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType.ActionType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Person Met">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "PersonMet.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Person Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "PersonMet.Designation.Designation")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
            <%--   </div>--%>
        </ContentTemplate>
    </asp1:TabPanel>
    <%-- <asp1:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
    </asp1:TabPanel>--%>
    <asp1:TabPanel ID="tpnlConversation" runat="server" HeaderText="Conversation" Visible="false">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvConversation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Progress Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Conversation" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblConversation" Text='<%# DataBinder.Eval(Container.DataItem, "Conversation")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conversation Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblConversationDate" Text='<%# DataBinder.Eval(Container.DataItem, "ConversationDate","{0:d}")%>' runat="server" />
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
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="VC Summary" Visible="false">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvEffortConversationVisit" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">

                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFollowUp" runat="server" HeaderText="Follow Up">
        <ContentTemplate>
            <%--<div class="col-md-12">--%>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Follow Up Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowUpDate" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Follow Up Note">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpNote")%>' runat="server" />
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
            <%--</div>--%>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFinancial" runat="server" HeaderText="Financial Info" Visible="false">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvFinancial" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBankName" Text='<%# DataBinder.Eval(Container.DataItem, "BankName.BankName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Finance Percentage" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinancePercentage" Text='<%# DataBinder.Eval(Container.DataItem, "FinancePercentage")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
    </asp1:TabPanel>
    <%-- <asp1:TabPanel ID="tpnlExpense" runat="server" HeaderText="Expense">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
    </asp1:TabPanel>--%>
    <asp1:TabPanel ID="tpnlProduct" runat="server" HeaderText="Product">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    <asp:Label ID="lblLeadProductID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadProductID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Product")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ref Quotation">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.RefQuotationNo")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblRefQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.RefQuotationDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sap Quotation">
                                <ItemTemplate>
                                    <asp:Label ID="lblSapQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.SapQuotationNo")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblSapQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.SapQuotationDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parts Quotation">
                                <ItemTemplate>
                                    <asp:Label ID="lblPgQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.PgQuotationNo")%>' runat="server" />
                                    <br />
                                    <asp:Label ID="lblPgQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotation.PgQuotationDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update Status">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAddQuotation" runat="server" OnClick="lbActions_Click">Convert to Quotation</asp:LinkButton>
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
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Support Document">
        <ContentTemplate>
            <div class="col-md-12">
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUpload" runat="server" /></td>
                        <td>
                            <asp:Button ID="btnAddFile" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFile_Click" /></td>
                    </tr>
                </table>
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSupportDocument" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" />
                                        <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbSupportDocumentDownload" runat="server" OnClick="lbSupportDocumentDownload_Click">Download </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbSupportDocumentDelete" runat="server" OnClick="lbSupportDocumentDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlQuestionaries" runat="server" HeaderText="Questionaries" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <%--  <div class="col-md-12">--%>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvQuestionaries" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    <asp:Label ID="lblLeadQuestionariesID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadQuestionariesID")%>' runat="server" Visible="false" />

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attribute Main">
                                <ItemTemplate>
                                    <asp:Label ID="lblAttributeMain" Text='<%# DataBinder.Eval(Container.DataItem, "QuestionariesMain.LeadQuestionariesMain")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attribute Sub">
                                <ItemTemplate>
                                    <asp:Label ID="lblAttributeSub" Text='<%# DataBinder.Eval(Container.DataItem, "QuestionariesSub.LeadQuestionariesSub")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbMarketSegmentDelete" runat="server" OnClick="lbMarketSegmentDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
            <%--  </div>--%>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>



<asp:Panel ID="pnlSEAssign" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Assign Engineer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAssignEngineer" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_AssignSE ID="UC_AssignSE" runat="server"></UC:UC_AssignSE>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveAssignSE" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveAssignSE_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AssignSE" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSEAssign" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlFollowUp" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales FollowUp</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a></div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageFollowUp" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_FollowUp ID="UC_FollowUp" runat="server"></UC:UC_FollowUp>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveFollowUp" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveFollowUp_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_FollowUp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFollowUp" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlConversation" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales Conversation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a></div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageConversation" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_CustomerConversation ID="UC_CustomerConversation" runat="server"></UC:UC_CustomerConversation>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveustomerConversation" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveustomerConversation_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Conversation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlConversation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlFinancial" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales Financial Info</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a></div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageFinancial" runat="server" Text="" CssClass="message" Visible="false" />
        <UC:UC_Financial ID="UC_Financial" runat="server"></UC:UC_Financial>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveFinancial" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveFinancial_Click" />
        </div>
    </div>

</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Financial" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFinancial" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageEffort" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_Effort ID="UC_Effort" runat="server"></UC:UC_Effort>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveEffort" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveEffort_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageExpense" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_Expense ID="UC_Expense" runat="server"></UC:UC_Expense>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveExpense" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveExpense_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlProduct" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Product</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageProduct" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_Product ID="UC_Product" runat="server"></UC:UC_Product>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveProduct" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveProduct_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Product" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlProduct" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlQuotation" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Quotation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageQuotation" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_Quotation ID="UC_Quotation" runat="server"></UC:UC_Quotation>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="BtnSaveQuotation" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveQuotation_Click"></asp:Button>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Quotation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlQuotation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlLostReason" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Lost Reason</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageLost" runat="server" Text="" CssClass="message" ForeColor="Red" />
            <div class="col-md-2 text-right">
                <label>Reason</label>
            </div>
            <div class="col-md-10">
                <asp:TextBox ID="txtLostReason" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnLostReasonUpdate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnLostReasonUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_LeadLost" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLostReason" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlLeadDrop" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cancel Reason</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button9" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageDrop" runat="server" Text="" CssClass="message" ForeColor="Red" />
            <div class="col-md-2 text-right">
                <label>Reason</label>
            </div>
            <div class="col-md-10">
                <asp:TextBox ID="txtRejectedBySalesReason" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnRejectedBySalesUpdate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnLeadDropUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_LeadDrop" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLeadDrop" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlLead" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Lead</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button10" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageLead" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="fldCountry" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                <UC:UC_AddLead ID="UC_AddLead" runat="server"></UC:UC_AddLead>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnLeadEdit" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnLeadEdit_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Lead" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLead" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAddQuestionaries" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Questionaries</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button11" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageQuestionaries" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Questionaries Main</label>
                        <asp:DropDownList ID="ddlQuestionariesMain" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlQuestionariesMain_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Questionaries Sub</label>
                        <asp:DropDownList ID="ddlQuestionariesSub" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remark</label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveMarketSegment" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveMarketSegment_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Questionaries" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddQuestionaries" BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
    <Animations> 
         
            <OnShown>
                <FadeIn Duration="2.5"    />
                 
            </OnShown>  
         
            <OnHiding>
                <FadeOut Duration=".5"  />
            </OnHiding>
            <OnHidden>
                <FadeOut Duration=".5"   />
            </OnHidden>
        
    </Animations>
</ajaxToolkit:ModalPopupExtender>
<%--<ajaxToolkit:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="lbtAddQuestionaries">
        <Animations>
                <OnClick>
                    <Parallel AnimationTarget="pnlAddQuestionaries"                Duration="0.3" Fps="25">
                    <FadeIn />                                        
                    <Move   Vertical="250"></Move>
                    </Parallel>                   
                </OnClick>
        </Animations>
    </ajaxToolkit:AnimationExtender>--%>

<asp:Panel ID="pnlVisit" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Visit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button12" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageColdVisit" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border">
                <div class="col-md-12">

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Cold Visit Date</label>
                        <asp:TextBox ID="txtVisitDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:CalendarExtender ID="ceVisitDate" runat="server" TargetControlID="txtVisitDate" PopupButtonID="txtVisitDate" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtVisitDate" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Action Type</label>
                        <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Importance</label>
                        <asp:DropDownList ID="ddlImportance" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Location</label>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Person Met</label>
                        <asp:DropDownList ID="ddlPersonMet" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remark</label>
                        <asp:TextBox ID="txtVisitRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveVisit" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveVisit_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Visit" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlVisit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEditExpectedDate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Expected Date Of Sale</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button13" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
             <asp:Label ID="lblExpectedDateOfSaleMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-6 col-sm-12">
                <label>Expected Date of Sale</label>
                <asp:TextBox ID="txtExpectedDateOfSale" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                <asp1:CalendarExtender ID="cxExpectedDateOfSale" runat="server" TargetControlID="txtExpectedDateOfSale" PopupButtonID="txtExpectedDateOfSale" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtExpectedDateOfSale" WatermarkText="DD/MM/YYYY" />
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnUpdateExpectedDate" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateExpectedDate_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditExpectedDate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditExpectedDate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlEditNextFollowUpDate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Next Follow Up Date</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button14" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblNextFollowUpDateMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-6 col-sm-12">
                <label>Next Follow Up Date</label>
                <asp:TextBox ID="txtEditNextFollowUpDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                <asp1:CalendarExtender ID="cxEditNextFollowUpDate" runat="server" TargetControlID="txtEditNextFollowUpDate" PopupButtonID="txtEditNextFollowUpDate" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextEditNextFollowUpDate" runat="server" TargetControlID="txtEditNextFollowUpDate" WatermarkText="DD/MM/YYYY" />
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnUpdateNextFollowUpDate" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateNextFollowUpDate_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditNextFollowUpDate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditNextFollowUpDate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <%-- <div  >--%>
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<%--  <asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Pre -Sales Effort</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sales Engineer">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlEffortTypeF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortDateF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>

                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortStartTimeF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEffortEndTimeF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effort" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarkF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblEffortAdd" runat="server" OnClick="lblEffortAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>--%>

<%-- <asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Pre -Sales Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sales Engineer">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSalesEngineerF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlExpenseTypeF" runat="server" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtExpenseDateF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmountF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarkF" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblExpenseAdd" runat="server" OnClick="lblExpenseAdd_Click">Add</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </asp:Panel>--%>

<script type="text/javascript">
    $(document).ready(function () {
        var gvFollowUp = document.getElementById('MainContent_gvProduct');
        if (gvFollowUp != null) {
            for (var i = 0; i < gvFollowUp.rows.length - 1; i++) {
                var lblFollowUpStatusID = document.getElementById('MainContent_gvProduct_lblFollowUpStatus_' + i);
                var divActions = document.getElementById('divActions' + i);
                if (lblFollowUpStatusID.innerHTML != "Open") {
                    divActions.style.display = "none";
                }
            }
        }
    });
</script>


<script type="text/javascript">

    function GetProjectAuto() {
        debugger;
        var parentElement = window.parent.document.getElementById("myElement");
        alert(window.parent.document);
        $("#MainContent_UC_LeadView_UC_AddLead_hdfProjectID").val('');
        var param = { Pro: $('#MainContent_UC_LeadView_UC_AddLead_txtProject').val() }
        var Customers = [];
        if ($('#MainContent_UC_LeadView_UC_AddLead_txtProject').val().trim().length >= 3) {
            $.ajax({
                url: "LeadN.aspx/GetProject",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    for (i = 0; i < DataList.length; i++) {
                        Customers[i] = {
                            value: DataList[i].ProjectName,
                            ProjectID: DataList[i].ProjectID,
                            TenderNumber: DataList[i].TenderNumber,
                            State: DataList[i].State.State,
                            District: DataList[i].District.District,
                            ProjectValue: DataList[i].Value,
                            ContractAwardDate: DataList[i].ContractAwardDate,
                            ContractEndDate: DataList[i].ContractEndDate
                        };
                    }
                    $('#MainContent_UC_LeadView_UC_AddLead_txtProject').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {


                            $("#MainContent_UC_LeadView_UC_AddLead_hdfProjectID").val(u.item.ProjectID);
                            document.getElementById("MainContent_UC_LeadView_UC_AddLead_txtProject").disabled = true;
                            //document.getElementById('divCustomerViewID').style.display = "block";
                            //document.getElementById('divCustomerCreateID').style.display = "none";

                            //document.getElementById('lblCustomerName').innerText = u.item.value;
                            //document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                            //document.getElementById('lblMobile').innerText = u.item.Mobile;

                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_LeadView_UC_AddLead_txtProject').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {

                        var inner_html = FormatAutocompleteList(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };

                }
            });
        }
        else {
            $('#MainContent_UC_LeadView_UC_AddLead_txtProject').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }
</script>
