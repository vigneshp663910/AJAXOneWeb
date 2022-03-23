<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.LeadView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<%@ Register Src="~/ViewPreSale/UserControls/AssignSE.ascx" TagPrefix="UC" TagName="UC_AssignSE" %>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddCustomerConvocation.ascx" TagPrefix="UC" TagName="UC_CustomerConvocation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFinancial.ascx" TagPrefix="UC" TagName="UC_Financial" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLeadProduct.ascx" TagPrefix="UC" TagName="UC_Product" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddQuotation.ascx" TagPrefix="UC" TagName="UC_Quotation" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnEditLead" runat="server" OnClick="lbActions_Click">Edit Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnConvertToProspect" runat="server" OnClick="lbActions_Click">Convert to Prospect</asp:LinkButton>
                <asp:LinkButton ID="lbtnLostLead" runat="server" OnClick="lbActions_Click">Lost Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnCancelLead" runat="server" OnClick="lbActions_Click">Cancel Lead</asp:LinkButton>
                <asp:LinkButton ID="lbtnAssign" runat="server" OnClick="lbActions_Click">Assign</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFollowUp" runat="server" OnClick="lbActions_Click">Add Follow-up</asp:LinkButton>
                <asp:LinkButton ID="lbtnCustomerConvocation" runat="server" OnClick="lbActions_Click">Customer Convocation</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddEffort" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddExpense" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFinancialInfo" runat="server" OnClick="lbActions_Click">Financial Info</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddQuotation" runat="server" OnClick="lbActions_Click">Convert to Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbtAddQuestionaries" runat="server" OnClick="lbActions_Click">Add Questionaries</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Lead Number : </label>
                <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Lead Date : </label>
                <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Category : </label>
                <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Progress Status : </label>
                <asp:Label ID="lblProgressStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Qualification : </label>
                <asp:Label ID="lblQualification" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Source : </label>
                <asp:Label ID="lblSource" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Type : </label>
                <asp:Label ID="lblType" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Remarks : </label>
                <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Contact Person : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Mobile : </label>
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Email : </label>
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Address : </label>
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Financial Info : </label>
                <asp:Label ID="lblFinancialInfo" runat="server" CssClass="label"></asp:Label>
            </div>

        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlSalesEngineer" runat="server" HeaderText="Sales Engineer" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvSalesEngineer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
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
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlConvocation" runat="server" HeaderText="Convocation">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvConvocation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
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
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Convocation" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblConvocation" Text='<%# DataBinder.Eval(Container.DataItem, "Convocation")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Convocation Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblConvocationDate" Text='<%# DataBinder.Eval(Container.DataItem, "ConvocationDate","{0:d}")%>' runat="server" />
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
    <asp1:TabPanel ID="tpnlFinancial" runat="server" HeaderText="Financial Info">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvFinancial" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlExpense" runat="server" HeaderText="Expense">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
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
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlProduct" runat="server" HeaderText="Product">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Product")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server" />
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
     <asp1:TabPanel ID="tpnlQuestionaries" runat="server" HeaderText="Questionaries" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvQuestionaries" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId">
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
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


<asp:Panel ID="pnlConvocation" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales Convocation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a></div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageConvocation" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_CustomerConvocation ID="UC_CustomerConvocation" runat="server"></UC:UC_CustomerConvocation>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveustomerConvocation" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveustomerConvocation_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Convocation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlConvocation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


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
            <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
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
<ajaxToolkit:ModalPopupExtender ID="MPE_LostReason" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLostReason" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlRejectedBySales" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Rejected By Sales Reason</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button9" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="Label2" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-2 text-right">
                <label>Reason</label>
            </div>
            <div class="col-md-10">
                <asp:TextBox ID="txtRejectedBySalesReason" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnRejectedBySalesUpdate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnRejectedBySalesUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_RejectedBySales" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlRejectedBySales" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

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
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label>Lead Date</label>
                        <asp:TextBox ID="txtLeadDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Progress Status</label>
                        <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="Category" DataValueField="CategoryID" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Qualification</label>
                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" DataTextField="Qualification" DataValueField="QualificationID" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" DataTextField="Source" DataValueField="SourceID" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Lead Type</label>
                        <asp:DropDownList ID="ddlLeadType" runat="server" CssClass="form-control" DataTextField="Status" DataValueField="StatusID" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

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
<ajaxToolkit:ModalPopupExtender ID="MPE_Questionaries" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddQuestionaries" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
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
 
       