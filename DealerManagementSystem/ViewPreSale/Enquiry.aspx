<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Enquiry.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Enquiry" %>


<%@ Register Src="~/ViewPreSale/UserControls/EnquiryView.ascx" TagPrefix="UC" TagName="UC_EnquiryView" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />


    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                       <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"   />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Enquiry Number</label>
                        <asp:TextBox ID="txtSEnquiryNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer Name</label>
                        <asp:TextBox ID="txtSCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server"   CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"  />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"   AutoPostBack="true"  OnSelectedIndexChanged="ddlState_SelectedIndexChanged"  />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
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
                    <div class="col-md-2 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="BtnSearch_Click" />
                        <asp:Button ID="BtnAdd" runat="server" Text="Add Enquiry" CssClass="btn Save" Width="100px" OnClick="BtnAdd_Click" />
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">

                <%--<div class="col-md-12">--%>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Enquiry(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEnqArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnEnqArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEnqArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnEnqArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenEnquiryID" runat="server" />
                            <asp:GridView ID="gvEnquiry" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="5" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvEnquiry_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Enquiry Number" DataField="EnquiryNumber"></asp:BoundField>
                                    <asp:BoundField HeaderText="Enquiry Date" DataField="EnquiryDate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName"></asp:BoundField>
                                    <asp:BoundField HeaderText="PersonName" DataField="PersonName"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Mail")%>'><%# DataBinder.Eval(Container.DataItem, "Mail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Mail">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate> 
                                    </ItemTemplate>
                                </asp:TemplateField> <asp:BoundField HeaderText="Country" DataField="Country.Country"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="State" DataField="State.State"></asp:BoundField>
                                    <asp:BoundField HeaderText="District" DataField="District.District"></asp:BoundField>
                                    <asp:BoundField HeaderText="Address" DataField="Address"></asp:BoundField>
                                    <asp:BoundField HeaderText="Product" DataField="Product"></asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
                                    <asp:BoundField HeaderText="Source" DataField="Source.Source"></asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status.Status"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryID")%>' CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />
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
                <%--</div>--%>
              
            </div>
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div> 
            <UC:UC_EnquiryView ID="UC_EnquiryView" runat="server"></UC:UC_EnquiryView>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlAddEnquiry" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblAddEnquiryMessage" runat="server"  CssClass="message" Visible="false" />
                <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AddEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

</asp:Content>
