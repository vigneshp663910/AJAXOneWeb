<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="CustomerGstApproval.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.CustomerGstApproval" %>

<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">From</label>
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">To</label>
                            <asp:TextBox ID="txtTo" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Status</label>
                            <asp:DropDownList ID="ddlIsApproved" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                <asp:ListItem Value="2">Rejected</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
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
                                                <td>Customer GST Approval(s):</td>
                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnCustArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnCustArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvCustomerGSTApproval" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCustomerGSTApproval_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                            <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="GST" DataField="GSTIN"></asp:BoundField>
                                    <asp:BoundField HeaderText="PAN" DataField="PAN"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Approved">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved" Text='<%# Eval("IsApproved") == null ? "" : Eval("IsApproved").ToString() == "False" ? "Rejected" : "Approved" %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="IsApproved" DataField="IsApproved"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="ApprovedBy" DataField="ApprovedBy.ContactName"></asp:BoundField>
                                    <asp:BoundField HeaderText="ApprovedOn" DataField="ApprovedOn"></asp:BoundField>
                                    <asp:BoundField HeaderText="CreatedBy" DataField="CreatedBy.ContactName"></asp:BoundField>
                                    <asp:BoundField HeaderText="CreatedOn" DataField="CreatedOn"></asp:BoundField>
                                    <asp:BoundField HeaderText="SendSAP" DataField="SendSAP"></asp:BoundField>
                                    <asp:BoundField HeaderText="Success" DataField="Success"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Back" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CustomerGSTApprovalID")%>' OnClick="btnView_Click" Width="75px" Height="25px" />
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
        </div>
        <div class="col-md-12" id="divView" runat="server" visible="false">
            <div class="col-md-12">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="action-btn">
                            <div class="" id="boxHere"></div>
                            <div class="dropdown btnactions" id="customerAction">
                                <div class="btn Approval">Actions</div>
                                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                    <asp:LinkButton ID="lbApproveCustomerGST" runat="server" OnClick="lbActions_Click">Approve</asp:LinkButton>
                                    <asp:LinkButton ID="lbRejectCustomerGST" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 field-margin-top">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Customer GST Approval</legend>
                            <div class="col-md-12 View">
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>Customer Name : </label>
                                        <asp:Label ID="lblCustomerName" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>GSTIN : </label>
                                        <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
                                    </div>                                    
                                    <div class="col-md-12">
                                        <label>ApprovedBy : </label>
                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>CreatedBy : </label>
                                        <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>OldGSTIN : </label>
                                        <asp:Label ID="lblOldGst" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>PAN : </label>
                                        <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
                                    </div>                                    
                                    <div class="col-md-12">
                                        <label>ApprovedOn : </label>
                                        <asp:Label ID="lblApprovedOn" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>CreatedOn : </label>
                                        <asp:Label ID="lblCreatedOn" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>OldPAN : </label>
                                        <asp:Label ID="lblOldPan" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>IsApproved : </label>
                                        <asp:Label ID="lblIsApproved" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>ApprovedRemark : </label>
                                        <asp:Label ID="lblApprovedRemark" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <asp1:TabContainer ID="tbpContainer" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
                <asp1:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer Info">
                    <ContentTemplate>
                        <div class="col-md-12 field-margin-top">
                            <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
                        </div>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tpnlSupportDocument" runat="server" HeaderText="Support Document">
                    <ContentTemplate>
                        <div class="col-md-12">
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
            </asp1:TabContainer>
        </div>
        <asp:Panel ID="pnlApproveCustomerGST" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Approve Customer GST</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button10" runat="server" Text="X" CssClass="PopupClose" />
                </a>
            </div>
            <div class="col-md-12">
                <div class="model-scroll">
                    <asp:Label ID="lblMessageApproveCustomerGST" runat="server" Text="" CssClass="message" Visible="false" />
                    <fieldset class="fieldset-border" id="fldCountry" runat="server">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Approve Customer GST</legend>
                        <div class="col-md-12">
                            <div class="col-md-12 col-sm-12">
                                <label>Approver Remarks</label>
                                <asp:TextBox ID="txtApproverRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnApproveCustomerGST" runat="server" Text="Approve" CssClass="btn Save" OnClick="btnApproveCustomerGST_Click" />
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_ApproveCustomerGST" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlApproveCustomerGST" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
        <div style="display: none">
            <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
    </div>
</asp:Content>
