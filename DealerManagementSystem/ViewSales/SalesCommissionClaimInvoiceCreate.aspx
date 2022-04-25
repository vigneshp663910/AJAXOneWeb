<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesCommissionClaimInvoiceCreate.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesCommissionClaimInvoiceCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewSales/UserControls/SalesCommissionClaimView.ascx" TagPrefix="UC" TagName="UC_ClaimView" %>
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
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Claim Number</label>
                            <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp1:TextBoxWatermarkExtender>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp1:TextBoxWatermarkExtender>
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
                                                <td>Quotation(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnQuoteArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnQuoteArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnQuoteArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnQuoteArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="Claim Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalesCommissionClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 1 By" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved1By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1By.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 1 On">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved1On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1On")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 2 By" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved2By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2By.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 2 On">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved2On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2On")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 3 By" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved3By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3By.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved 3 On">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved3On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3On")%>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnCreateInvoice" runat="server" Text="Create Invoice" CssClass="btn Back" OnClick="btnCreateInvoice_Click" Width="120px" Height="33px" />
                                            <asp:Button ID="btnViewQuotation" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewQuotation_Click" Width="50px" Height="33px" />
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
                </div>
            </div>
        </div>

        <div class="col-md-12" id="divColdVisitView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_ClaimView ID="UC_ClaimView" runat="server"></UC:UC_ClaimView>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>

