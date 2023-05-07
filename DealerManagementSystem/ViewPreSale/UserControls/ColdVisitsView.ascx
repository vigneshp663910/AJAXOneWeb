<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColdVisitsView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.ColdVisitsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AssignSE.ascx" TagPrefix="UC" TagName="UC_AssignSE" %>--%>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AddCustomerConversation.ascx" TagPrefix="UC" TagName="UC_CustomerConversation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFinancial.ascx" TagPrefix="UC" TagName="UC_Financial" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLeadProduct.ascx" TagPrefix="UC" TagName="UC_Product" %>--%>

<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>

    <%--var geocoder;--%>

    <style>
        .fieldset-borderAuto {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }

            .fieldset-borderAuto tr {
                /* background-color: #000084; */
                background-color: inherit;
                font-weight: bold;
                color: white;
            }

            .fieldset-borderAuto:hover {
                background-color: blue;
            }
    </style>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
               <%-- <asp:LinkButton ID="lbtnAddEffort" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddExpense" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>--%>
                <asp:LinkButton ID="lbtnStatusChangeToClose" runat="server" OnClick="lbActions_Click">Status Change to Close</asp:LinkButton>
                <asp:LinkButton ID="lbtnStatusChangeToCancel" runat="server" OnClick="lbActions_Click">Status Change to Cancel</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddActivity" runat="server" OnClick="lbActions_Click">Add Activity</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Cold Visit</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Visit Number : </label>
                    <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Visit Date : </label>
                    <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PersonMet : </label>
                    <asp:Label ID="lblPersonMet" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Contact Person : </label>
                    <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                 <div class="col-md-12">
                    <label>Person Designation : </label>
                    <asp:Label ID="lblPersonDesignation" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Address : </label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Importance : </label>
                    <asp:Label ID="lblImportance" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Location : </label>
                    <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                </div>
                
            </div>
        </div>
    </fieldset>
</div>


 
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Activity">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" DataKeyNames="ActivityStartLatitude,ActivityStartLongitude,ActivityEndLatitude,ActivityEndLongitude">
                        <Columns>
                            <asp:TemplateField HeaderText="Activity Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityID" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityID")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityType" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityType.ActivityTypeName")%>' runat="server" />
                                    <asp:Label ID="lblActivityTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityType.ActivityTypeID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date" SortExpression="Start Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate","{0:d}")%>' runat="server" />--%>
                                    <asp:Label ID="lblStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate")%>' runat="server" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblEndDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityEndDate","{0:d}")%>' runat="server" />--%>
                                    <asp:Label ID="lblEndDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityEndDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expenses" SortExpression="ExpenseType">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType","{0:n}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses" SortExpression="Expenses">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount","{0:n}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses" SortExpression="EffortType">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType","{0:n}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EffortDuration" SortExpression="Expenses">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortDuration" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDuration","{0:n}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" SortExpression="Action">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Button ID="btnEndActivity" runat="server" Text="End Activity" CssClass="btn Back" OnClick="btnEndActivity_Click"
                                        Width="115px" Height="25px" />
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
   <%-- <asp1:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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
    </asp1:TabPanel>--%>

     

</asp1:TabContainer>

<asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
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
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
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
  
<asp:Panel ID="pnlAddActivity" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnAddActivityClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblAddActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Start Date</label>
                        <asp:Label ID="lblStartActivityDate" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Activity Type</label>
                        <asp:DropDownList ID="ddlActivityTypeS" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Location</label>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remarks</label>
                        <asp:TextBox ID="txtRemarksS" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnStartActivity" runat="server" Text="Start" CssClass="btn Save" OnClick="btnStartActivity_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlEndActivity" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEndActivity">End Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnEndActivityClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblEndActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:Label ID="lblValidationMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">

            <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Activity Number</label>
                        <asp:Label ID="lblActivityIDE" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Activity Type</label>
                        <asp:Label ID="lblActivityTypeE" runat="server" Text="" CssClass="message" />
                        <asp:Label ID="lblActivityTypeIDE" runat="server" Text="" CssClass="message" Visible="false" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">End Date</label>
                        <asp:Label ID="lblEndActivityDate" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Effort Type</label>
                        <asp:DropDownList ID="ddlEffortType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Effort Duration</label>
                        <asp:TextBox ID="txtEffortDuration" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Expense Type</label>
                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Expenses</label>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remarks</label>
                        <asp:TextBox ID="txtRemarkE" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnEndActivityE" runat="server" CssClass="btn Save" Text="End Activity" OnClick="btnEndActivityE_Click" Width="150px"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<asp1:ModalPopupExtender ID="MPE_EndActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEndActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlTrackActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueTrackActivity">Track Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <div id="map_canvas" style="width: 100%; height: 500px"></div>
            </div>
        </div>
    </asp:Panel>
    <asp1:ModalPopupExtender ID="MPE_TrackActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlTrackActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>



    <script type="text/javascript">


        var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
            zoom: 4.6,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]

            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: { url: data.image, scaledSize: new google.maps.Size(25, 25) },
            });

            (function (marker, data) {

                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                });
            })(marker, data);
        }

    </script>

   <asp:HiddenField ID="hfLatitudeAction" runat="server" />
   <asp:HiddenField ID="hfLongitudeAction" runat="server" />

  <script> 
      debugger;
      function success(position1) {
           const latitude1 = position1.coords.latitude;
           const longitude1 = position1.coords.longitude; 
          document.getElementById('MainContent_UC_ColdVisitsView_hfLatitudeAction').value = latitude1;
          document.getElementById('MainContent_UC_ColdVisitsView_hfLongitudeAction').value = longitude1;
          status.textContent = '';
      }
      function error() {
          status.textContent = 'Unable to retrieve your location';
      }

      if (!navigator.geolocation) {
          status.textContent = 'Geolocation is not supported by your browser';

      } else {
          status.textContent = 'Locating…';
          debugger;
          navigator.geolocation.getCurrentPosition(success, error);
      }
  </script>

