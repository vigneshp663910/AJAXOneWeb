<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketView.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketView" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnEditLead" runat="server" OnClick="lbActions_Click">Edit Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnAssign" runat="server" OnClick="lbActions_Click">Assign</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFollowUp" runat="server" OnClick="lbActions_Click">Add Follow-up</asp:LinkButton>
                <asp:LinkButton ID="lbtnCustomerConversation" runat="server" OnClick="lbActions_Click">Customer Conversation</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddEffort" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddExpense" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFinancialInfo" runat="server" OnClick="lbActions_Click">Financial Info</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddQuotation" runat="server" OnClick="lbActions_Click">Convert to Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbtAddQuestionaries" runat="server" OnClick="lbActions_Click">Add Questionaries</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddVisit" runat="server" OnClick="lbActions_Click">Add Visit</asp:LinkButton>
                <asp:LinkButton ID="lbtnLostLead" runat="server" OnClick="lbActions_Click">Lost Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnCancelLead" runat="server" OnClick="lbActions_Click">Cancel Lead</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>IC Ticket : </label>
                <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Requested Date : </label>
                 <asp:Label ID="lblRequestedDate" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>District : </label>
                  <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
            </div>
              <div class="col-md-4">
                <label>Complaint Description : </label> 
                  <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label> Status : </label> 
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Information : </label> 
                 <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Dealer : </label> 
                 <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Customer : </label>
                 <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Customer Category : </label> 
                 <asp:Label ID="lblCustomerCategory" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Contact Person Name & No : </label> 
                 <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Old IC Ticket Number : </label> 
                <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty : </label> 
                <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Is Margin Warranty : </label> 
                <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Equipment : </label> 
                <asp:Label ID="lblEquipment" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Model : </label> 
                <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty Expiry : </label> 
                <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Last HMR Date & Value : </label> 
                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Refurbished Expiry : </label> 
                <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>AMC Expiry : </label> 
                <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label> : </label> 

            </div>
            <div class="col-md-4">
                <label> : </label> 

            </div>
            <div class="col-md-4">
                <label> : </label> 

            </div>
        </div>
    </fieldset>
</div>         
                                                        
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
                </div>
            </div>
            <%--</div>--%>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlConversation" runat="server" HeaderText="Conversation">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFinancial" runat="server" HeaderText="Financial Info">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel> 
    <asp1:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlExpense" runat="server" HeaderText="Expense">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlProduct" runat="server" HeaderText="Product">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlQuestionaries" runat="server" HeaderText="Questionaries" Font-Bold="True" ToolTip="">
        <ContentTemplate> 
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div> 
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabVisit" runat="server" HeaderText="Visit" Font-Bold="True" ToolTip="">
        <ContentTemplate> 
            <div class="col-md-12 Report">
                <div class="table-responsive"> 
                </div>
            </div> 
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>                                                     