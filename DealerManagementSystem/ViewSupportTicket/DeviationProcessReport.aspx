<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DeviationProcessReport.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.DeviationProcessReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12" id="DivList" runat="server">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">FileName</label>
                        <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">FileType</label>
                        <asp:TextBox ID="txtFileType" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Subject</label>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
                        <asp:Button ID="btnCreate" runat="server" CssClass="btn Save" Text="Create" OnClick="btnCreate_Click"></asp:Button>
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
                                            <td>Deviation Process Report(s):</td>
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
                        <asp:GridView ID="gvDeviationProcess" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDeviationProcess_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FileName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DeviationProcessID")%>' OnClick="lnkFileName_Click"/>
                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" Visible="false"/>
                                        <asp:Label ID="lblDeviationProcessID" Text='<%# DataBinder.Eval(Container.DataItem, "DeviationProcessID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FileType">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                        <asp:Label ID="lblCreatedByUserID" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.UserID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requested By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedBy" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.ContactName")%>' runat="server" />
                                        <asp:Label ID="lblRequestedByUserID" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedBy.UserID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovedBy" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedBy.ContactName")%>' runat="server" />
                                        <asp:Label ID="lblApprovedByUserID" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedBy.UserID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModifiedBy" Text='<%# DataBinder.Eval(Container.DataItem, "ModifiedBy.ContactName")%>' runat="server" />
                                        <asp:Label ID="lblModifiedByUserID" Text='<%# DataBinder.Eval(Container.DataItem, "ModifiedBy.UserID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDeviationProcessDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DeviationProcessID")%>' OnClick="lblDeviationProcessDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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

    <div class="col-md-12" id="DivCreate" runat="server" visible="false">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Create Deviation Process</legend>
            <div class="col-md-12">
                <div class="col-md-6">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Requested By Info</legend>
                        <div class="col-md-12 col-sm-12">
                            <label>Dealer<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlRequestedByDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRequestedByDealer_SelectedIndexChanged" />
                            <label>Requested By<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlRequestedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label>Requested On<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtRequestedOn" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="cxRequestedOn" runat="server" TargetControlID="txtRequestedOn" PopupButtonID="txtRequestedOn" Format="dd/MM/yyyy" />
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtRequestedOn" WatermarkText="DD/MM/YYYY" />
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-6">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Approved By Info</legend>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlApprovedByDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovedByDealer_SelectedIndexChanged" />
                            <label class="modal-label">Approved By<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label>Approved On<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtApprovedOn" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="cxApprovedOn" runat="server" TargetControlID="txtApprovedOn" PopupButtonID="txtApprovedOn" Format="dd/MM/yyyy" />
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtApprovedOn" WatermarkText="DD/MM/YYYY" />
                        </div>
                    </fieldset>
                </div>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Other Info</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Subject<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtISubject" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Attachment<samp style="color: red">*</samp></label>
                            <asp:FileUpload ID="fuFileUpload" runat="server" ClientIDMode="Static" CssClass="TextBox form-control" />
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton btn Back" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" />
            </div>
        </fieldset>
    </div>
</asp:Content>
