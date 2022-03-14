<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.QuotationView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AssignSE.ascx" TagPrefix="UC" TagName="UC_AssignSE" %>--%>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/AddCustomerConvocation.ascx" TagPrefix="UC" TagName="UC_CustomerConvocation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFinancial.ascx" TagPrefix="UC" TagName="UC_Financial" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLeadProduct.ascx" TagPrefix="UC" TagName="UC_Product" %>--%>

<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Quotation</legend>

        <div class="col-md-12 View">
            <div class="col-md-2 text-right">
                <label>Visit Number</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblLeadNumber" runat="server"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Visit Date</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblLeadDate" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Dealer</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblDealer" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Remarks</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblRemarks" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Customer</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblCustomer" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Contact Person</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblContactPerson" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Mobile</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblMobile" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Email</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblEmail" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-2 text-right">
                <label>Location</label>
            </div>
            <div class="col-md-2">
                <asp:label id="lblLocation" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-6">
                <label>Importance : </label>
                <asp:label id="lblImportance" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-6">
                <label>Status : </label>
                <asp:label id="lblStatus" runat="server" cssclass="label"></asp:label>
            </div>
            <div class="col-md-12">
                <div style="float: right;">
                    <div class="dropdown">
                        <asp:button id="BtnActions" runat="server" cssclass="btn Approval" text="Actions" />
                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                            <asp:linkbutton id="lbActions" runat="server" onclick="lbActions_Click">Add Effort</asp:linkbutton>
                            <asp:linkbutton id="LinkButton1" runat="server" onclick="lbActions_Click">Add Expense</asp:linkbutton>
                            <asp:linkbutton id="lbtnStatusChangeToClose" runat="server" onclick="lbActions_Click">Status Change to Close</asp:linkbutton>
                            <asp:linkbutton id="lbtnStatusChangeToCancel" runat="server" onclick="lbActions_Click">Status Change to Cancel</asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:label id="lblMessage" runat="server" text="" cssclass="message" visible="false" />
<asp1:tabcontainer id="tbpCust" runat="server" tooltip="Geographical Location Master..." font-bold="True" font-size="Medium">
    <asp1:tabpanel id="tpnlEffort" runat="server" headertext="Effort" font-bold="True" tooltip="List of Countries...">
        <contenttemplate>
            <div class="col-md-12 Report">
                <asp:gridview id="gvEffort" runat="server" autogeneratecolumns="false" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found">
                    <columns>
                        <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center">
                            <itemtemplate>
                                <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:templatefield headertext="Sales Engineer">
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                            <itemtemplate>
                                <asp:label id="lblSEContactName" text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:templatefield headertext="Effort Type">
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                            <itemtemplate>
                                <asp:label id="lblEffortType" text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                            </itemtemplate>

                        </asp:templatefield>
                        <asp:templatefield headertext="Effort Date" sortexpression="Country">
                            <itemtemplate>
                                <asp:label id="lblEffortDate" text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:templatefield headertext="Effort Start Time" sortexpression="Country">
                            <itemtemplate>
                                <asp:label id="lblEffortStartTime" text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                            </itemtemplate>

                        </asp:templatefield>
                        <asp:templatefield headertext="Effort End Time" sortexpression="Country">
                            <itemtemplate>
                                <asp:label id="lblEffortEndTime" text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:templatefield headertext="Effort" sortexpression="Country">
                            <itemtemplate>
                                <asp:label id="lblEffort" text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:templatefield headertext="Remark" sortexpression="Country">
                            <itemtemplate>
                                <asp:label id="lblRemark" text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                            </itemtemplate>
                        </asp:templatefield>
                    </columns>
                    <alternatingrowstyle backcolor="#ffffff" />
                    <footerstyle forecolor="White" />
                    <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                    <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                    <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                </asp:gridview>
            </div>
        </contenttemplate>
    </asp1:tabpanel>

    <asp1:tabpanel id="tpnlExpense" runat="server" headertext="Expense">
        <contenttemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <asp:gridview id="gvExpense" runat="server" autogeneratecolumns="false" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found">
                        <columns>
                            <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center">
                                <itemtemplate>
                                    <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Sales Engineer">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:label id="lblSEContactName" text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Expense Type">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:label id="lblExpenseType" text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Expense Date" sortexpression="Country">
                                <itemtemplate>
                                    <asp:label id="lblExpenseDate" text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Amount" sortexpression="Country">
                                <itemtemplate>
                                    <asp:label id="lblAmount" text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Remark" sortexpression="Country">
                                <itemtemplate>
                                    <asp:label id="lblRemark" text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                        </columns>
                        <alternatingrowstyle backcolor="#ffffff" />
                        <footerstyle forecolor="White" />
                        <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                        <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                        <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                    </asp:gridview>
                </div>
            </div>
        </contenttemplate>
    </asp1:tabpanel>
    <asp1:tabpanel id="TabPanel1" runat="server" headertext="Support Document">
        <contenttemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Support Document</legend>

                <table>
                    <tr>
                        <td>
                            <asp:fileupload id="fileUpload" runat="server" />
                        </td>
                        <td>
                            <asp:button id="btnAddFile" runat="server" cssclass="btn Approval" text="Add" onclick="btnAddFile_Click" />
                        </td>
                    </tr>
                </table>
                <div class="col-md-12 Report">
                    <asp:gridview id="gvSupportDocument" runat="server" autogeneratecolumns="false" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found">
                        <columns>
                            <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center">
                                <itemtemplate>
                                    <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="File Name">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:label id="lblFileName" text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" />
                                    <asp:label id="lblAttachedFileID" text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" visible="false" />
                                    <asp:label id="lblFileType" text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" visible="false" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Date">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:label id="lblCreatedOn" text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Created By">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:label id="lblCreatedBy" text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Download">
                                <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                <itemtemplate>
                                    <asp:linkbutton id="lbSupportDocumentDownload" runat="server" onclick="lbSupportDocumentDownload_Click">Download </asp:linkbutton>
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:templatefield headertext="Action" headerstyle-width="50px" itemstyle-horizontalalign="Center">
                                <itemtemplate>
                                    <asp:linkbutton id="lbSupportDocumentDelete" runat="server" onclick="lbSupportDocumentDelete_Click"><i class="fa fa-fw fa-times" style="font-size: 18px"></i></asp:linkbutton>
                                </itemtemplate>
                            </asp:templatefield>
                        </columns>
                        <alternatingrowstyle backcolor="#ffffff" />
                        <footerstyle forecolor="White" />
                        <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                        <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                        <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                    </asp:gridview>

                </div>
            </fieldset>
        </contenttemplate>
    </asp1:tabpanel>
</asp1:tabcontainer>

<asp:panel id="pnlEffort" runat="server" cssclass="Popup" style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
            <asp:button id="Button1" runat="server" text="X" cssclass="PopupClose" />
        </a>
    </div>
    <asp:label id="lblMessageEffort" runat="server" text="" cssclass="message" visible="false" />
    <div class="col-md-12">

        <uc:uc_effort id="UC_Effort" runat="server"></uc:uc_effort>
        <div class="col-md-12 text-center">
            <asp:button id="btnSaveEffort" runat="server" text="Save" cssclass="btn Save" onclick="btnSaveEffort_Click" />
        </div>

    </div>
</asp:panel>

<ajaxtoolkit:modalpopupextender id="MPE_Effort" runat="server" targetcontrolid="lnkMPE" popupcontrolid="pnlEffort" backgroundcssclass="modalBackground" cancelcontrolid="btnCancel" />

<asp:panel id="pnlExpense" runat="server" cssclass="Popup" style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:button id="Button3" runat="server" text="X" cssclass="PopupClose" />
        </a>
    </div>

    <asp:label id="lblMessageExpense" runat="server" text="" cssclass="message" visible="false" />
    <div class="col-md-12">
        <uc:uc_expense id="UC_Expense" runat="server"></uc:uc_expense>
        <div class="col-md-12 text-center">
            <asp:button id="btnSaveExpense" runat="server" text="Save" cssclass="btn Save" onclick="btnSaveExpense_Click" />
        </div>

    </div>
</asp:panel>

<ajaxtoolkit:modalpopupextender id="MPE_Expense" runat="server" targetcontrolid="lnkMPE" popupcontrolid="pnlExpense" backgroundcssclass="modalBackground" cancelcontrolid="btnCancel" />

<div style="display: none">
    <asp:linkbutton id="lnkMPE" runat="server">MPE</asp:linkbutton><asp:button id="btnCancel" runat="server" text="Cancel" />
</div>
