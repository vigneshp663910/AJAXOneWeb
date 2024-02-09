<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerBalanceConfirmationReport.aspx.cs" Inherits="DealerManagementSystem.ViewFinance.Reports.DealerBalanceConfirmationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <fieldset class="fieldset-border" id="Fieldset2" runat="server">
        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
        <div class="col-md-12">
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Dealer</label>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">From Date</label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate" WatermarkText="DD/MM/YYYY" />
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">To Date</label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate" WatermarkText="DD/MM/YYYY" />
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Status</label>
                <asp:DropDownList ID="ddlBalanceConfirmationStatus" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                <asp:Button ID="btnRequestForMail" runat="server" Text="Request For Statement in  Mail" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnRequestForMail_Click" Width="220px"  />
            </div>
        </div>
    </fieldset>
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
                                        <td>Dealer Balance Confirmation Report:</td>
                                        <td>
                                            <asp:Label ID="lblRowCountDealerBalConFirm" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnDealerBalConFirmArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerBalConFirmArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnDealerBalConFirmArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerBalConFirmArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvDealerBalanceConfirmationRpt" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnRowDataBound="gvDealerBalanceConfirmationRpt_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                                    <asp:Label ID="lblDealerBalanceConfirmationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBalanceConfirmationID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Balance" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorBalance" Text='<%# DataBinder.Eval(Container.DataItem, "VendorBalance")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Balance As Per Dealer" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorBalanceAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "VendorBalanceAsPerDealer")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Balance" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerBalance" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerBalance" )%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Balance As Per Dealer" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerBalanceAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerBalanceAsPerDealer" )%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Outstanding As Per Ajax" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalOutstandingAsPerAjax" Text='<%# DataBinder.Eval(Container.DataItem, "TotalOutstandingAsPerAjax")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" Text='<%# DataBinder.Eval(Container.DataItem, "Currency" )%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Outstanding As Per Dealer" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalOutstandingAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "TotalOutstandingAsPerDealer")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Confirmation Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBalanceConfirmationStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Confirmation Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBalanceConfirmationOn" Text='<%# DataBinder.Eval(Container.DataItem, "BalanceConfirmationOn","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attachment">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None" CssClass="table table-bordered table-condensed Grid">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">
                                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBalanceConfirmationAttachmentID")%>' runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle ForeColor="White" BackColor="#fce4d6" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" BackColor="#fce4d6" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
    <asp:Panel ID="pnlMailRequest" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueReachedSite">Request For Mail</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button16" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblRequestForMail" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Request For Mail"</legend>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealerMail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Open Date</label>
                        <asp:TextBox ID="txtOpenDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Post Date From</label>
                        <asp:TextBox ID="txtPostDateFrom" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Mail</label>
                        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnRequest" runat="server" Text="Request" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnRequest_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_MailRequest" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMailRequest" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
