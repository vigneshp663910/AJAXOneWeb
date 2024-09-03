<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="OpenSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.OpenSupportTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewSupportTicket/UserControls/SupportTicketView.ascx" TagPrefix="UC" TagName="UC_SupportTicketView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12" id="divList" runat="server">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">From</label>
                        <asp:TextBox ID="txtRequestedDateFrom" runat="server" CssClass="TextBox form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">To</label>
                        <asp:TextBox ID="txtRequestedDateTo" runat="server" CssClass="TextBox form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Requested By</label>
                        <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Ticket ID</label>
                        <asp:TextBox ID="txtTicketId" runat="server" CssClass="TextBox form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
        <br />
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                <div class="col-md-12 Report">
                    <%--<div class="col-md-12" style="display:none">
                            <div class="col-md-2 col-sm-12">
                                <asp:RadioButton ID="rbAssign" runat="server" Text="Assign" GroupName="ss" CssClass="label" Checked="true" />
                            </div>
                            <div class="col-md-3 col-sm-12">
                                <asp:RadioButton ID="rbSendForApproval" runat="server" Text="Send for Approval" GroupName="ss" CssClass="label" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <asp:RadioButton ID="rbReject" runat="server" Text="Reject" GroupName="ss" CssClass="label" />
                            </div>
                        </div>--%>
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Task(s):</td>

                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvTickets_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibMessage" runat="server" Width="30px" OnClick="ibMessage_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket ID">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested By">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Requested On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" Text='<%# DataBinder.Eval(Container.DataItem, "MobileNo")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Age">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" Text='<%# DataBinder.Eval(Container.DataItem, "age")%>' runat="server"></asp:Label>
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
    <div class="col-md-12" id="divSupportTicketView" runat="server" visible="false">
        <div class="col-md-12 lead-back-btn">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
        </div>
        <UC:UC_SupportTicketView ID="UC_SupportTicketView" runat="server"></UC:UC_SupportTicketView>
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>


    <%--<div class="col-md-12" id="pnView" runat="server" visible="false">
        <div class="form-container-fields">
            <span class="field-label">Send For Approval Form</span>
            <div class="row">
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblRequestedOn" runat="server" Text="Requested By" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label5" runat="server" Text="Category" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label"></asp:Label>

                    <asp:TextBox ID="txtStatus" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-12 col-sm-6">
                    <asp:Label ID="lblTicketDescription" runat="server" Text="Ticket Description" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="70px" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label6" runat="server" Text="Ticket Type" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtTicketType" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label8" runat="server" Text="Approver" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="ddlapprovar" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label7" runat="server" Text="Attached File" CssClass="label"></asp:Label>
                    <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle BorderStyle="None" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("text") %>' CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" CssClass="InputButton btn Save" Width="200px" OnClick="btnSendForApproval_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="Go Back" CssClass="InputButton btn Save" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </div>--%>

    <%--<div class="col-md-12" id="pnlReject" runat="server" visible="false">
        <div class="form-container-fields">
            <span class="field-label">Reject Form</span>
            <div class="row">

                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label9" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtTicketNoReject" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-12 col-sm-6">
                    <asp:Label ID="Label13" runat="server" Text="Description" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtTicketNoRejectRemark" runat="server" TextMode="MultiLine" CssClass="TextBox form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="InputButton btn Save" OnClick="btnReject_Click" />
                    <asp:Button ID="btnRejectBack" runat="server" Text="Go Back" CssClass="InputButton btn Save" OnClick="btnRejectBack_Click" />
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>

