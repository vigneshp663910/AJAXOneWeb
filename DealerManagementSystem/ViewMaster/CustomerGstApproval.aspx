<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="CustomerGstApproval.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.CustomerGstApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
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
                                                <td>Customer Change Approval(s):</td>
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
                            <asp:GridView ID="gvCustomerChangeForApproval" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCustomerChangeForApproval_PageIndexChanging">
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
                                    <asp:BoundField HeaderText="IsApproved" DataField="IsApproved"></asp:BoundField>
                                    <asp:BoundField HeaderText="ApprovedBy" DataField="ApprovedBy.ContactName"></asp:BoundField>
                                    <asp:BoundField HeaderText="ApprovedOn" DataField="ApprovedOn"></asp:BoundField>
                                    <asp:BoundField HeaderText="CreatedBy" DataField="CreatedBy.ContactName"></asp:BoundField>
                                    <asp:BoundField HeaderText="CreatedOn" DataField="CreatedOn"></asp:BoundField>
                                    <asp:BoundField HeaderText="SendSAP" DataField="SendSAP"></asp:BoundField>
                                    <asp:BoundField HeaderText="Success" DataField="Success"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Back" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CustomerChangeForApprovalID")%>' OnClick="btnView_Click" Width="75px" Height="25px" />
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Customer Change For Approval</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Customer Name : </label>
                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Unregistered : </label>
                                    <asp:Label ID="lblUnregistered" runat="server" CssClass="label"></asp:Label>
                                </div>                                
                                <div class="col-md-12">
                                    <label>IsApproved : </label>
                                    <asp:Label ID="lblIsApproved" runat="server" CssClass="label"></asp:Label>
                                </div>                                
                                <div class="col-md-12">
                                    <label>CreatedBy : </label>
                                    <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>SendSAP : </label>
                                    <asp:Label ID="lblSendSAP" runat="server" CssClass="label"></asp:Label>
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
                                    <label>CreatedOn : </label>
                                    <asp:Label ID="lblCreatedOn" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Success : </label>
                                    <asp:Label ID="lblSuccess" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>PAN : </label>
                                    <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>ApprovedOn : </label>
                                    <asp:Label ID="lblApprovedOn" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
