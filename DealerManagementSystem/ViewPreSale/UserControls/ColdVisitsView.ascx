<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColdVisitsView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.ColdVisitsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AssignSE.ascx" TagPrefix="UC" TagName="UC_AssignSE" %>--%>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AddCustomerConvocation.ascx" TagPrefix="UC" TagName="UC_CustomerConvocation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFinancial.ascx" TagPrefix="UC" TagName="UC_Financial" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLeadProduct.ascx" TagPrefix="UC" TagName="UC_Product" %>--%>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Cold Visit</legend>

        <div class="col-md-12 View">
            <div class="col-md-2 text-right">
                <label>Visit Number</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Visit Date</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Dealer</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Remarks</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Customer</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Contact Person</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Mobile</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Email</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Location</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Importance : </label>
                <asp:Label ID="lblImportance" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div style="float: right;">
                    <div class="dropdown">
                        <asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />
                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                            <asp:LinkButton ID="lbActions" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>
                            <asp:LinkButton ID="lbtnStatusChangeToClose" runat="server" OnClick="lbActions_Click">Status Change to Close</asp:LinkButton>
                            <asp:LinkButton ID="lbtnStatusChangeToCancel" runat="server" OnClick="lbActions_Click">Status Change to Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
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
                        <asp:TemplateField HeaderText="Effort Type">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
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
        </ContentTemplate>
    </asp1:TabPanel>

    <asp1:TabPanel ID="tpnlExpense" runat="server" HeaderText="Expense">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
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
                                    <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
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
    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Support Document">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Support Document</legend>

                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUpload" runat="server" /></td>
                        <td>
                            <asp:Button ID="btnAddFile" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFile_Click" /></td>
                    </tr>
                </table>
                <div class="col-md-12 Report">
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
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>

<asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageEffort" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">

        <UC:UC_Effort ID="UC_Effort" runat="server"></UC:UC_Effort>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveEffort" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveEffort_Click" />
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <asp:Label ID="lblMessageExpense" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <UC:UC_Expense ID="UC_Expense" runat="server"></UC:UC_Expense>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveExpense" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveExpense_Click" />
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
