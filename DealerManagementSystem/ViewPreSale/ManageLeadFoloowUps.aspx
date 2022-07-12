<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ManageLeadFoloowUps.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.ManageLeadFoloowUps" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <%--  <div class="col-md-2 col-sm-12"  style="display: none">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlFollow" runat="server" CssClass="form-control">
                                <asp:ListItem>Today's Follow-ups</asp:ListItem>
                                <asp:ListItem>Future Follow-ups</asp:ListItem>
                                <asp:ListItem>Completed Follow-ups</asp:ListItem>
                                <asp:ListItem>Cancelled Follow-ups</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12"  style="display: none">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                <asp:ListItem>My Own Follow-ups</asp:ListItem>
                                <asp:ListItem>Assigned Follow-ups</asp:ListItem>
                                <asp:ListItem>Team Follow-ups</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>

                        </div>
                        <%--    <div class="col-md-2 col-sm-12"  >
                            <label class="modal-label">Mobile</label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">

                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Follow Up:</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnFUArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnFUArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnFUArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnFUArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                            <asp:Label ID="lblLeadFollowUpID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadFollowUpID")%>' runat="server" Visible="false" />
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
                                            <asp:Label ID="lblFollowUpNote" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpNote")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lead Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.LeadNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lead Date" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.LeadDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Category.Category")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.ProgressStatus.ProgressStatus")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Qualification" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Qualification.Qualification")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Source.Source")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Status.Status")%>' runat="server" />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Type.Type")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Dealer Code" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Dealer.DealerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FollowUp Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFollowUpStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                             <asp:Label ID="lblFollowUpStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update Status" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlActions" runat="server"  >
                                                <div class="dropdown">
                                                    <div class="btn Approval" style="height: 25px">Actions</div>
                                                    <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                                        <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Close</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlFoloowUpStatus" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Foloow Up Status</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>
        <asp:Label ID="lblMessageResponsible" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="col-md-12">
                <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Remark</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnFoloowUpStatus" runat="server" Text="Save" CssClass="btn Save" OnClick="btnFoloowUpStatus_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_FoloowUpStatus" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFoloowUpStatus" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>


</asp:Content>

